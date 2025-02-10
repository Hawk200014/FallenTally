using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Utility.ResultSets
{
    public class ResultSet<T>
    {
        #region Result Enum

        #endregion

        private RESULT _result;
        private string _message;
        private T _data;

        public ResultSet(RESULT result, T data, string message = "")
        {
            _result = result;
            if (_result == RESULT.SUCCESS && message == "")
            {
                _message = "Success";
            }
            else
            {
                _message = message;
            }
            _data = data;
        }

        public T GetData()
        {
            return _data;
        }

        public string GetMessage()
        {
            return _message;
        }

        public RESULT GetResult()
        {
            return _result;
        }

    }
}
