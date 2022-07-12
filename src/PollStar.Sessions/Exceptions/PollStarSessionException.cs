using PollStar.Core.Exceptions;
using PollStar.Sessions.ErrorCodes;

namespace PollStar.Sessions.Exceptions;

public class PollStarSessionException : PollStarException
{
    internal PollStarSessionException(PollStarSessionErrorCode errorCode, string message, Exception? ex = null) : base(errorCode, message, ex)
    {
    }
}