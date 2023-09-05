using System;

namespace WebSystemMvc.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public string Message { get; set; }

        public IntegrityException(string message) : base(message) { }
    }
}
