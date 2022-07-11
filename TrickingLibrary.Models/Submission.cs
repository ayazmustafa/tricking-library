namespace TrickingLibrary.Models;

public class Submission : BaseModel
{
    public string Video { get; set; }
    public string Description { get; set; }

    public int TrickId { get; set; }
}