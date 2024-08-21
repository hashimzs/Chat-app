namespace Library.Services
{
    using Microsoft.JSInterop;
    using System;
    using System.Threading.Tasks;

    public class StompService
    {
        private readonly IJSRuntime _jsRuntime;

        public StompService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ConnectAsync(string url, object headers, Func<string, Task> onConnected, Func<string, Task> onError,string token)
        {
            await _jsRuntime.InvokeVoidAsync("stompClient.connect", url, headers,
                DotNetObjectReference.Create(new CallbackHelper(onConnected)),
                DotNetObjectReference.Create(new CallbackHelper(onError)),
                token);

        }

        public async Task DisconnectAsync()
        {
            await _jsRuntime.InvokeVoidAsync("stompClient.disconnect");
        }

        public async Task<IJSObjectReference> SubscribeAsync(string destination, Func<string, Task> callback)
        {
            var jsObjectReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("stompClient.subscribe", destination,
                DotNetObjectReference.Create(new CallbackHelper(callback)));

            return jsObjectReference;
        }

        public async Task SendAsync(string destination, object headers, string body)
        {
            await _jsRuntime.InvokeVoidAsync("stompClient.send", destination, headers, body);
        }

        private class CallbackHelper
        {
            private readonly Func<string, Task> _callback;

            public CallbackHelper(Func<string, Task> callback)
            {
                _callback = callback;
            }

            [JSInvokable]
            public async Task Callback(string message)
            {
                await _callback(message);
            }
        }
    }

}
