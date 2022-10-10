namespace WebShop.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries().Where(e => e.Entity is IEntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    ((IEntityBase)entityEntry.Entity).Created = DateTime.Now;
                    break;
                case EntityState.Modified:
                    ((IEntityBase)entityEntry.Entity).Modified = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries().Where(e => e.Entity is IEntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    ((IEntityBase)entityEntry.Entity).Created = DateTime.Now;
                    break;
                case EntityState.Modified:
                    ((IEntityBase)entityEntry.Entity).Modified = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }


    // Add ApplicationUser
    public DbSet<ApplicationUser> ApplicationUser { get; set; }

    // Add Address
    public DbSet<Address> Address { get; set; }

    // Add Hero
    public DbSet<Hero> Hero { get; set; }

    // Add Product
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }

    // Add ShoppingChart
    public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
    public DbSet<ShoppingCart> ShoppingCart { get; set; }

    // Add Order
    public DbSet<Order> Order { get; set; }

    //Add ToDoList
    public DbSet<Models.Dbo.Task> Task { get; set; }
    public DbSet<ToDoList> ToDoList { get; set; }

    // Add Blog
    public DbSet<Blog> Blog { get; set; }
}