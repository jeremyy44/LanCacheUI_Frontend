namespace DeveLanCacheUI_Frontend.TimeZoneComponent
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrowserTimeProvider(this IServiceCollection services)
            => services.AddScoped<TimeProvider, BrowserTimeProvider>();
    }
}
