namespace TrickingLibrary.Models;

public class Trick : BaseModel<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DifficultyId { get; set; }
    // todo do we need this?
    public Difficulty Difficulty { get; set; }

    public ICollection<TrickRelationship> Prerequisites { get; set; }
    public ICollection<TrickRelationship> Progressions { get; set; }

    public ICollection<TrickCategory> TrickCategories { get; set; }
}

public class TrickCategory
{
    public string TrickId { get; set; }
    public Trick Trick { get; set; }

    public string CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category : BaseModel<string>
{
    public string Description { get; set; }
    public ICollection<TrickCategory> Tricks { get; set; }
}

public class TrickRelationship
{
    public string PrerequisiteId { get; set; }
    public Trick Prerequisite { get; set; }
    public string ProgressionId { get; set; }
    public Trick Progression { get; set; }
}

public class Difficulty : BaseModel<string>
{
    public string Description { get; set; }
    public ICollection<Trick> Tricks { get; set; }
}