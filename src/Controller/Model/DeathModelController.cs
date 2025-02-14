using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FallenTally.Utility.ResultSets;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Controller.Model
{
    public class DeathModelController
    {
        private SQLiteDBContext? _context;
        private Singleton _singleton = Singleton.GetInstance();

        public DeathModelController()
        {
            _context = _singleton.GetValue("DBContext") as SQLiteDBContext;
        }

        #region Getter


        public ResultSet<DeathModel?> GetItem(int deathId)
        {
            DeathModel? death = _context?.Deaths.Find(deathId);

            if(death is null)
            {
                return new ResultSet<DeathModel?>(RESULT.FAILURE, null, "Death not found");
            }

            return new ResultSet<DeathModel?>(RESULT.SUCCESS, death);
        }

        public ResultSet<DeathModel?> GetItem(DeathModel filter)
        {
            return GetItem(filter.DeathId);
        }

        public ResultSet<List<DeathModel>?> GetItems()
        {
            List<DeathModel>? deaths = _context?.Deaths.ToList();
            if (deaths is null)
            {
                return new ResultSet<List<DeathModel>?>(RESULT.FAILURE, null, "No deaths found");
            }
            return new ResultSet<List<DeathModel>?>(RESULT.SUCCESS, deaths);
        }

        public ResultSet<List<DeathModel>?> GetItems(DeathLocationModel filter)
        {
            List<DeathModel>? deaths = _context?.Deaths.Where(d => d.LocationId == filter.LocationId).ToList();
            if (deaths is null)
            {
                return new ResultSet<List<DeathModel>?>(RESULT.FAILURE, null, "No deaths found");
            }
            return new ResultSet<List<DeathModel>?>(RESULT.SUCCESS, deaths);
        }

        public ResultSet<List<DeathModel>?> GetItems(DateOnly date)
        {
            List<DeathModel>? deaths = _context?.Deaths.Where(d => DateOnly.FromDateTime(d.TimeStamp) == date).ToList();
            if (deaths is null)
            {
                return new ResultSet<List<DeathModel>?>(RESULT.FAILURE, null, "No deaths found");
            }
            return new ResultSet<List<DeathModel>?>(RESULT.SUCCESS, deaths);

        }

        public ResultSet<List<DeathModel>?> GetItems(DateTime date)
        {
            return GetItems(DateOnly.FromDateTime( date ));
        }

        #endregion

        #region Adder

        public ResultSet<DeathModel?> AddItem(DeathModel death)
        {
            _context?.Deaths.Add(death);
            _context?.SaveChanges();
            return new ResultSet<DeathModel?>(RESULT.SUCCESS, death);
        }

        #endregion

        #region Remover

        public ResultSet<int> RemoveItem(int deathId)
        {
            ResultSet<DeathModel?> model = GetItem(deathId);
            if(model.GetResult() == RESULT.FAILURE)
            {
                return new ResultSet<int>(RESULT.FAILURE, -1, "Death not found");
            }
            return RemoveItem(model.GetData()!);
        }

        public ResultSet<int> RemoveItem(DeathModel deathFilter)
        {
            
            if (GetItem(deathFilter).GetResult() == RESULT.FAILURE)
            {
                return new ResultSet<int>(RESULT.FAILURE, -1, "Death not found");
            }
            _context?.Deaths.Remove(deathFilter);
            _context?.SaveChanges();
            return new ResultSet<int>(RESULT.SUCCESS, 0);
        }

        #endregion

        #region Updater

        public ResultSet<DeathModel?> UpdateItem(DeathModel deathFilter)
        {
            RESULT result = GetItem(deathFilter).GetResult();
            if (result == RESULT.FAILURE)
            {
                return new ResultSet<DeathModel?>(RESULT.FAILURE, null, "Death not found");
            }
            _context?.Deaths.Update(deathFilter);
            _context?.SaveChanges();
            return new ResultSet<DeathModel?>(RESULT.SUCCESS, deathFilter);
        }

        #endregion
    }
}
