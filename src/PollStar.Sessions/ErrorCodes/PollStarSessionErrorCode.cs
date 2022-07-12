using PollStar.Core.ErrorCodes;

namespace PollStar.Sessions.ErrorCodes;

public abstract class PollStarSessionErrorCode : PollStarErrorCode
{
    public static readonly PollStarSessionErrorCode SessionNotFound = new SessionNotFoundErrorCode();
    public static readonly PollStarSessionErrorCode SessionPersistenceFailed = new SessionPersistenceFailedErrorCode();
    public static readonly PollStarSessionErrorCode SessionNameInvalid = new SessionNameInvalidErrorCode();
    public static readonly PollStarSessionErrorCode SessionDescriptionInvalid = new SessionDescriptionInvalidErrorCode();

    public override string TranslationKey => $"Errors.Sessions.{Code}";
}