namespace TrickingLibrary.Models;

public class Category : BaseModel<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<TrickCategory> Tricks { get; set; }
}