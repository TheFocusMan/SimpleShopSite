using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SexyWebSite.Shared
{
    public class ToolTipPageHub : ComponentBase,IAsyncDisposable
    {
        Lazy<Task<IJSObjectReference>> _siteScripts;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        public bool Disposed { get; private set; }

        public async virtual ValueTask DisposeAsync()
        {
            if (!Disposed)
            {
                try
                {
                    if (_siteScripts.IsValueCreated)
                    {
                        var module = await _siteScripts.Value;
                        await module.DisposeAsync();
                    }
                }
                catch (Exception)
                {
                }
                Disposed = true; // safe coding
            }
        }

        public ToolTipPageHub()
        {
            _siteScripts = new(() => JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/boostraphelper.js").AsTask());
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var module = await _siteScripts.Value;
            await module.InvokeVoidAsync("CheckForTooltips");
            if (firstRender) await UpdatePopOvers();
        }

        public async ValueTask UpdatePopOvers()
        {
            var module = await _siteScripts.Value;
            await module.InvokeVoidAsync("CheckForPopOvers");
        }

        public async ValueTask<object> GetToolTipFromElement(ElementReference refe)
        {
            var module = await _siteScripts.Value;
            return await module.InvokeAsync<object>("GetToolTipFromElement", refe);
        }
    }
}
