using OnlineOrdering.Common.Models.Errors;

namespace OnlineOrdering.Common.Models.Responses
{
    public class ErrorResponse
    {
        public IList<Error> Errors { get; set; } = new List<Error>();
    }
}
