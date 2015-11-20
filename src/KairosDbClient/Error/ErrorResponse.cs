using System.Collections.Generic;

namespace KairosDbClient.Error
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } 
    }
}