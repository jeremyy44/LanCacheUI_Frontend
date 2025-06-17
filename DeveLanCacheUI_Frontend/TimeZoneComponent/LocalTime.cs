using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Globalization;

namespace DeveLanCacheUI_Frontend.TimeZoneComponent
{
    public sealed class LocalTime : ComponentBase, IDisposable
    {
        [Inject]
        public TimeProvider TimeProvider { get; set; } = default!;
        private BrowserTimeProvider? _browserTimeProvider;

        [Parameter]
        public DateTime? DateTime { get; set; }

        protected override void OnInitialized()
        {
            if (TimeProvider is BrowserTimeProvider browserTimeProvider)
            {
                browserTimeProvider.LocalTimeZoneChanged += LocalTimeZoneChanged;
                _browserTimeProvider = browserTimeProvider;
            }
            else
            {
                throw new InvalidOperationException("TimeProvider must be of type BrowserTimeProvider.");
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (DateTime != null)
            {
                var browserTimeProvider = TimeProvider as BrowserTimeProvider;

                builder.AddContent(0, TimeProvider.ToLocalDateTime(DateTime.Value).ToString(browserTimeProvider?.DateTimeFormat));
            }
        }

        public void Dispose()
        {
            if (TimeProvider is BrowserTimeProvider browserTimeProvider)
            {
                browserTimeProvider.LocalTimeZoneChanged -= LocalTimeZoneChanged;
            }
        }

        private void LocalTimeZoneChanged(object? sender, EventArgs e)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }
}
