namespace WebApp.Models;

public class CommentAddModel
{
  public Guid PostId { get; set; }
  public Guid UserId { get; set; }
  public string Text { get; set; } = default!;
}