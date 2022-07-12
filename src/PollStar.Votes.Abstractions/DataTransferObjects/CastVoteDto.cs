namespace PollStar.Votes.Abstractions.DataTransferObjects;

public class CastVoteDto
{
    public Guid ConnectionId { get; set; }
    public Guid PollId { get; set; }
    public Guid OptionId { get; set; }
    public DateTimeOffset CastedOn { get; set; }
}