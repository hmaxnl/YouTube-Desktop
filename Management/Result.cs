using System;

namespace Management
{
    public class Result
    {
        public Result(ResultCode code, string? message = null, Exception? exception = null)
        {
            Code = code;
            Message = message;
            Exception = exception;
        }
        public ResultCode Code { get; }
        public string? Message { get; }
        public Exception? Exception { get; }
    }

    public enum ResultCode
    {
        Ok,
        Warning,
        Error,
        Fatal
    }
}