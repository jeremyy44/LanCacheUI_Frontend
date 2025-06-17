using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeveLanCacheUI_Frontend.TimeZoneComponent
{
    public sealed class InitializeTimeZone : ComponentBase
    {
        [Inject] public TimeProvider TimeProvider { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && TimeProvider is BrowserTimeProvider browserTimeProvider && !browserTimeProvider.IsLocalTimeZoneSet)
            {
                try
                {
                    await using var module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./timezone.js");
                    var timeZone = await module.InvokeAsync<string>("getBrowserTimeZone");
                    browserTimeProvider.SetBrowserTimeZone(timeZone);
                    var dateTimeFormat = await module.InvokeAsync<string>("getUserDateTimePattern");
                    browserTimeProvider.DateTimeFormat = dateTimeFormat;
                }
                catch (JSDisconnectedException)
                {
                }
            }
        }
    }
}
