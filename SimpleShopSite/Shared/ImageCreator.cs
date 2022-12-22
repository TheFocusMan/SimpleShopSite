using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace SexyWebSite.Shared
{
    public class ImageCreator : IAsyncDisposable
    {
        Lazy<Task<IJSObjectReference>> _siteScripts;
        List<string> _createdUrls = new List<string>();

        public ImageCreator(IJSRuntime runtime)
        {
            _siteScripts = new(() => runtime.InvokeAsync<IJSObjectReference>("import","./js/site.js").AsTask());  // השגת תסריט
        }

        public async ValueTask<string> CreateUrlFromStream(DotNetStreamReference stream)
        {
            var module = await _siteScripts.Value;
            var url = await module.InvokeAsync<string>("CreateObjectUrlFromStream", stream);
            _createdUrls.Add(url);
            return url;
        }

        public async ValueTask DisposeUrl(string url)
        {
            if (url != null)
            {
                try
                {
                    var module = await _siteScripts.Value;
                    _createdUrls.Remove(url);
                    await module.InvokeVoidAsync("disposeObject", url);
                }
                catch (Exception)
                {
                }
            }
        }
        public bool Disposed { get; private set; }

        public async ValueTask DisposeAsync()
        {
            if (!Disposed)
            {
                foreach (var url in _createdUrls.ToArray())
                    await DisposeUrl(url);

                if (_siteScripts.IsValueCreated)
                {
                    var module = await _siteScripts.Value;
                    await module.DisposeAsync();
                }
                Disposed = true;
            }
        }
    }
}
