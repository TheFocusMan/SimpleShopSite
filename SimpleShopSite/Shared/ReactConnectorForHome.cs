using Microsoft.JSInterop;

namespace SexyWebSite.Shared
{
    public class ReactConnectorForHome : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ReactConnectorForHome(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_framework/home/index.bundle.js").AsTask());
        }

        public async ValueTask Run()
        {
            await moduleTask.Value; // בישביל הרצה
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
