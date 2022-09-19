namespace WebShop.Services.Implementation;

public class QueueProcessor : BackgroundService
{
    private readonly IServiceScopeFactory scopeFactory;

    public QueueProcessor(IServiceScopeFactory scopeFactory)
    {
        this.scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var productService = scope.ServiceProvider.GetService<IProductService>();
                if (productService != null)
                {
                    await productService.UpdateShoppinCartStatus();
                }
            }
            // 1 Minute = 60,000 Milliseconds
            // 5 Minutes = 300,000 Milliseconds
            await Task.Delay(300000, stoppingToken);
        }
    }
}