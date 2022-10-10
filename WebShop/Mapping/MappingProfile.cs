using WebShop.Models.Dto;

namespace WebShop.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Hero
        CreateMap<HeroBinding, Hero>();
        CreateMap<Hero, HeroViewModel>();
        CreateMap<HeroViewModel, HeroBinding>();

        // Blog
        CreateMap<BlogBinding, Blog>();
        CreateMap<Blog, BlogViewModel>();
        CreateMap<BlogViewModel, BlogBinding>();


        CreateMap<IdentityRole, ApplicationUserViewModel>();
        CreateMap<IdentityRole, ApplicationUserRoleViewModel>();

        CreateMap<UserUpdateBinding, ApplicationUser>();
        CreateMap<UserUpdateBinding, ApplicationUserViewModel>();


        // ApplicationUser
        CreateMap<ApplicationUserBinding, ApplicationUser>();
        CreateMap<ApplicationUserUpdateBinding, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserViewModel>();
        CreateMap<ApplicationUser, ApplicationUserUpdateBinding>();


        CreateMap<ApplicationUserViewModel, ApplicationUserUpdateBinding>();
        CreateMap<UserBinding, ApplicationUser>()
            .ForMember(dst => dst.UserName, opts => opts.MapFrom(src => src.Email))
            .ForMember(dst => dst.EmailConfirmed, opts => opts.MapFrom(src => true));

        // Address
        CreateMap<AddressBinding, Address>();
        CreateMap<AddressUpdateBinding, Address>();
        CreateMap<Address, AddressViewModel>();
        CreateMap<AddressViewModel, AddressUpdateBinding>();

        // Product
        CreateMap<ProductBinding, Product>();
        CreateMap<ProductUpdateBinding, Product>();
        CreateMap<Product, ProductViewModel>();
        CreateMap<ProductViewModel, ProductUpdateBinding>();
        
        // ProductCategory
        CreateMap<ProductCategoryBinding, ProductCategory>();
        CreateMap<ProductCategoryUpdateBinding, ProductCategory>();
        CreateMap<ProductCategory, ProductCategoryViewModel>();

        // ProductImages
        CreateMap<ProductImagesBinding, ProductImages>();
        CreateMap<ProductImages, ProductImagesViewModel>();



        // ShoppingCartItem
        CreateMap<ShoppingCartItemBinding, ShoppingCartItem>();
        CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();

        // ShoppingCart
        CreateMap<ShoppingCart, ShoppingCartViewModel>();

        // Order
        CreateMap<Order, OrderViewModel>();



        // ToDoList
        CreateMap<ToDoListBinding, ToDoList>();
        CreateMap<ToDoListUpdateBinding, ToDoList>();
        CreateMap<ToDoList, ToDoListViewModel>();
        CreateMap<ToDoListViewModel, ToDoListUpdateBinding>();

        // Task
        CreateMap<TaskBinding, Models.Dbo.Task>();
        CreateMap<TaskUpdateBinding, Models.Dbo.Task>();
        CreateMap<Models.Dbo.Task, TaskViewModel>();
        CreateMap<TaskViewModel, TaskUpdateBinding>();
    }
}