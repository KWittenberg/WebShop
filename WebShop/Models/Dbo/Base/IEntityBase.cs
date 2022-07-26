namespace WebShop.Models.Dbo.Base;

public interface IEntityBase
{
    [Key]
    public int Id { get; set; }
    public DateTime Created { get; set; }
}