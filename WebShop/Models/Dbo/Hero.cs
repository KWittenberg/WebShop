namespace WebShop.Models.Dbo;

public class Hero: HeroBase, IEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
