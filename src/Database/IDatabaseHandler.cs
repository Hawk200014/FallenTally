using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database
{
    public interface IDatabaseHandler
    {
        public bool AddElementToDb(object element);

        public bool RemoveElementFromDb(object element);

        public bool UpdateElementFromDb(object element);

        public bool AddRangeOfElementsToDb(IEnumerable<object> elements);

        public bool RemoveRangeOfElementsToDb(IEnumerable<object> elements);

        public bool UpdateRangeOfElementsToDb(IEnumerable<object> elements);

        public 

        public IDatabaseHandler Connect();



    }
}
