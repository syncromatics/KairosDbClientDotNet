using System;
using System.Collections.Generic;

namespace KairosDbClient.Error
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public BadRequestException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
