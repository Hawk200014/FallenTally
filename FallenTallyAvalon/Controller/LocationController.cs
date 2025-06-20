using FallenTally.Database;
using FallenTally.Database.Models;
using FallenTally.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallenTally.Controller
{
    public class LocationController
    {
        private SQLiteDBContext _context;


        public LocationController( SQLiteDBContext context)
        {
            _context = context;
        }


        public bool AddLocation(GameStatsModel? gameModel, string locationName)
        {
            if (gameModel == null) return false;
            if (string.IsNullOrEmpty(locationName)) return false;
            if (IsDupeName(gameModel, locationName)) return false;
            _context.Locations.Add(new DeathLocationModel()
            {
                Name = locationName,
                GameID = gameModel.GameId
            }) ;
            _context.SaveChanges();
            return true;
        }


        public bool EditName(GameStatsModel? gameStatsModel, DeathLocationModel? locationModel, string editText)
        {
            if(gameStatsModel == null) return false;
            if (IsDupeName(gameStatsModel, editText)) return false;
            if (locationModel == null) return false;
            locationModel.Name = editText;
            _context.SaveChanges();
            return true;
        }


        public List<DeathLocationModel> GetListOfLocations(GameStatsModel? gameStatsModel)
        {
            if (gameStatsModel == null) return new List<DeathLocationModel>();
            return _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ToList();
        }


        internal bool IsDupeName(GameStatsModel? gameStatsModel, string editText)
        {
            if (gameStatsModel == null) return false;
            return _context.Locations.Where(x => x.Name.Equals(editText) && x.GameID == gameStatsModel.GameId).Any();
        }


        internal bool RemoveLocation(DeathLocationModel? locationModel)
        {
            if (locationModel == null) return false;
            if (locationModel.Name.Equals(GLOBALVARS.DEFAULT_LOCATION)) return false;
            _context.Deaths.Where(x => x.LocationId == locationModel.LocationId).ExecuteDelete();
            _context.Locations.Remove(locationModel);
            _context.SaveChanges();
            return true;
        }


        internal int GetDeathsAtLocation(DeathLocationModel? locationModel)
        {
            if(locationModel == null) return 0;
            return _context.Deaths.Where(x => x.LocationId == locationModel.LocationId).Count();
        }


        internal void RemoveAllLocations(GameStatsModel? gameStatsModel)
        {
            if(gameStatsModel == null) return;
            
            List<DeathLocationModel> locations = _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ToList();
            foreach(DeathLocationModel location in locations)
            {
                _context.Deaths.Where(x => x.LocationId == location.LocationId).ExecuteDelete();
            }
            _context.Locations.Where(x => x.GameID == gameStatsModel.GameId).ExecuteDelete();
            _context.SaveChanges();
        }

        internal bool GetFinishState(GameStatsModel? gameStatsModel, string oldValue)
        {
            if (gameStatsModel == null) return false;
            DeathLocationModel? location = _context.Locations.Where(x => x.Name.Equals(oldValue) && x.GameID == gameStatsModel.GameId).FirstOrDefault();
            if (location == null) return false;
            return location.Finish;
        }

        internal void SetFinish(DeathLocationModel? deathLocationModel,  bool? finished)
        {
            if (deathLocationModel == null) return;
            if (finished == null) return;
            deathLocationModel.Finish = (bool)finished;
            _context.SaveChanges();
        }
    }
}
