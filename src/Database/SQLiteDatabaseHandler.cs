using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database
{
    internal class SQLiteDatabaseHandler : IDatabaseHandler
    {
        public bool AddElementToDb(object element)
        {
            throw new NotImplementedException();
        }

        public bool AddRangeOfElementsToDb(IEnumerable<object> elements)
        {
            throw new NotImplementedException();
        }

        public IDatabaseHandler Connect()
        {
            throw new NotImplementedException();
        }

        public bool RemoveElementFromDb(object element)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRangeOfElementsToDb(IEnumerable<object> elements)
        {
            throw new NotImplementedException();
        }

        public bool UpdateElementFromDb(object element)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRangeOfElementsToDb(IEnumerable<object> elements)
        {
            throw new NotImplementedException();
        }
    }
}
