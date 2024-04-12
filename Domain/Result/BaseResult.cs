using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Result
{
    public class BaseResult
    {
        public bool isSuccses => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public int? ErrorCode { get; set; }

    }
    public class BaseResult<T> : BaseResult
    {
        public BaseResult(string errorMassage, int errorCode, T data)
        {
            ErrorMessage = errorMassage;
            ErrorCode = errorCode;
            Data = data;
        }
        public BaseResult() { }
        public T Data { get; set; }
    }
}
