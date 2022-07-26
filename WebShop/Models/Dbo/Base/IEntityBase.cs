namespace WebShop.Models.Dbo.Base;

public interface IEntity : IEntityBase
{
    [Key]
    public int Id { get; set; }
}

public interface IEntityBase
{
    public DateTime Created { get; set; }
}