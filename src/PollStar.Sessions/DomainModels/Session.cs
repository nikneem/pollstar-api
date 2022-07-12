using HexMaster.DomainDrivenDesign;
using HexMaster.DomainDrivenDesign.ChangeTracking;
using PollStar.Sessions.Abstractions.DomainModels;
using PollStar.Sessions.ErrorCodes;
using PollStar.Sessions.Exceptions;

namespace PollStar.Sessions.DomainModels;

public class Session : DomainModel<Guid>, ISession
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public void SetName(string value)
    {
        if (IsNullOrEmpty(value) || value.Length > 30)
        {
            throw new PollStarSessionException(PollStarSessionErrorCode.SessionNameInvalid,
                "The name of the session is invalid. It must contain at least one and max. 30 characters");
        }

        if (!Equals(Name, value))
        {
            Name = value;
            SetState(TrackingState.Modified);
        }
    }
    public void SetDescription(string? value)
    {
        if ( value != null && value.Length > 150)
        {
            throw new PollStarSessionException(PollStarSessionErrorCode.SessionDescriptionInvalid,
                "The name of the session is invalid. It may contain a max. of 150 characters");
        }
        if (!Equals(Description, value))
        {
            Description = value;
            SetState(TrackingState.Modified);
        }
    }


    public Session(Guid id, Guid userId, string name, string? description) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
    }
    public Session(Guid userId, string name) : base(Guid.NewGuid(), TrackingState.New)
    {
        UserId = userId;
        Name = name;
    }
}