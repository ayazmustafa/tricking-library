namespace TrickingLibrary.Models;

public class Trick : BaseModel<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Difficulty { get; set; }

    public ICollection<TrickRelationship> Prerequisites { get; set; }
    public ICollection<TrickRelationship> Progressions { get; set; }

    public ICollection<TrickCategory> TrickCategories { get; set; }
}