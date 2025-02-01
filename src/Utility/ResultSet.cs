using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Utility
{
    public class ResultSet<T>
    {
        #region Result Enum
        public enum Result
        {
            SUCCESS = 0,
            FAILURE = 1
        }
        #endregion

        private Result _result;
        private string _message;
        private T _data;
                     
        public ResultSet(Result result, T data, string message = "")
        {
            _result = result;
            if (_result == Result.SUCCESS && message == "")
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

        public Result GetResult()
        {
            return _result;
        }

    }
}
