namespace PollStar.Core.ErrorCodes;

public abstract class PollStarErrorCode
{
    public abstract string Code { get; }
    public virtual string TranslationKey => Code;
}