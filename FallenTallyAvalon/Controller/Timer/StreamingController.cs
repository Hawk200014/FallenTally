using FallenTally.Database;
using FallenTally.Database.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using FallenTallyAvalon.Models;
using FallenTally.Controller;

namespace FallenTally.Controller.Timers
{
    public class StreamingController : TimerController
    {
        private SQLiteDBContext _context;

        // Timer for checking stream status
        private System.Timers.Timer? _streamStatusTimer;
        private CancellationTokenSource? _streamStatusCts;

        // Event to notify when stream goes offline
        public event Action? StreamWentOffline;

        public StreamingController(SQLiteDBContext context)
        {
            _context = context;
        }

        public void AddStream()
        {
            if (_context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type.Equals("stream")).Count() == 0)
            {
                _context.Recordings.Add(new RecordingModel
                {
                    SessionCount = 1,
                    SessionDate = DateOnly.FromDateTime(DateTime.Now),
                    Type = "stream"
                });
            }
            else
            {
                var recording = _context.Recordings.Where(x => x.SessionDate == DateOnly.FromDateTime(DateTime.Now) && x.Type.Equals("stream")).First();
                recording.SessionCount++;
                _context.Recordings.Update(recording);
            }
            _context.SaveChanges();
        }

        public int GetStreamingNumber()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var recording = _context.Recordings
                .Where(x => x.SessionDate == today && x.Type.Equals("stream"))
                .FirstOrDefault();
                
            return recording?.SessionCount ?? 0;
        }

        /// <summary>
        /// Starts a background timer that checks every minute if the Twitch stream is offline.
        /// If the stream is offline, the StreamWentOffline event is raised.
        /// </summary>
        public void StartStreamStatusMonitor()
        {
            StopStreamStatusMonitor();

            _streamStatusCts = new CancellationTokenSource();
            _streamStatusTimer = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _streamStatusTimer.Elapsed += async (s, e) => await CheckStreamStatusAsync(_streamStatusCts.Token);
            _streamStatusTimer.AutoReset = true;
            _streamStatusTimer.Start();
        }

        /// <summary>
        /// Stops the background stream status monitor.
        /// </summary>
        public void StopStreamStatusMonitor()
        {
            _streamStatusCts?.Cancel();
            _streamStatusCts = null;
            if (_streamStatusTimer != null)
            {
                _streamStatusTimer.Stop();
                _streamStatusTimer.Dispose();
                _streamStatusTimer = null;
            }
        }

        private async Task CheckStreamStatusAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            int result = await TwitchInfoController.GetCurrentLiveTimeSecondsAsync();
            if (result < 0)
            {
                // Stream is offline or error occurred
                StreamWentOffline?.Invoke();
                StopStreamStatusMonitor();
            }
        }
    }
}
