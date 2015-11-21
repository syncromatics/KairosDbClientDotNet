using System;
using System.Collections.Generic;

namespace KairosDbClient.Error
{
    /// <summary>
    /// thrown for a Bad Request response (400) returned from KairosDb
    /// </summary>
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public BadRequestException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
