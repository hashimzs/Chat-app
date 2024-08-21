using Blazored.LocalStorage;
using Library.Dtos;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace Library.Services
{
    public class ChatStompService
    {
        private readonly StompService stomp;
        private readonly ILocalStorageService localStorageService;
        public Action<MessageDto> OnMessageRecieved;

        public ChatStompService(StompService stomp,ILocalStorageService localStorageService)
        {
            this.stomp = stomp;
            this.localStorageService = localStorageService;
        }
        public async Task Connect()
        {
            var token = await this.localStorageService.GetItemAsync<string>("token");
            var headers = new { Authorization = $"Bearer {token}" };

            await stomp.ConnectAsync("http://localhost:5000/chat-websocket", headers, OnConnected, OnError,token);
        }

        private async Task OnConnected(string frame)
        {
            Console.WriteLine("Connected: " + frame);

            await stomp.SubscribeAsync("/topic/public", async (messageBody) =>
            {
                var message = JsonConvert.DeserializeObject<MessageDto>(messageBody);
                DisplayMessage(message);
            });

            await stomp.SubscribeAsync("/user/topic/public", async (messageBody) =>
            {
                var message = JsonConvert.DeserializeObject<MessageDto>(messageBody);
                DisplayMessage(message);
            });
        }

        private async Task OnError(string error)
        {
            Console.WriteLine("Error: " + error);
        }

        public void DisplayMessage(MessageDto message)
        {
            if (OnMessageRecieved != null)
                this.OnMessageRecieved.Invoke(message);

        }


    }
}
