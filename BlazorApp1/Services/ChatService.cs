namespace Library.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Library.Dtos;
    using Microsoft.AspNetCore.Components.Forms;

    public class ChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MessageDto> SendMessageAsync(SendMessageDto chatMessage)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/chat", chatMessage);
            return await response.Content.ReadFromJsonAsync<MessageDto>();
        }

        public async Task<MessageDto> SendMessageWithImageAsync(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            content.Add(fileContent, "file", file.Name);

            var response = await _httpClient.PostAsync("/api/chat/image", content);
            return await response.Content.ReadFromJsonAsync<MessageDto>();
        }

        public async Task<List<MessageDto>> GetPublicMessagesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MessageDto>>("/api/chat/public");
        }

        public async Task<List<MessageDto>> GetMessagesByUserIdAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<MessageDto>>($"/api/chat/{userId}");
        }

        public async Task ReadUserMessagesAsync(int userId)
        {
            await _httpClient.PostAsync($"/api/chat/{userId}/read", null);
        }

        public async Task<MessageDto> SendMessageWithImageToUserAsync(IBrowserFile file, int userId)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            content.Add(fileContent, "file", file.Name);

            var response = await _httpClient.PostAsync($"/api/chat/{userId}/image", content);
            return await response.Content.ReadFromJsonAsync<MessageDto>();
        }

        public async Task<List<ChatDto>> GetChatsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ChatDto>>("/api/chat");
        }

        public async Task<MessageDto> SendMessageToUser(SendMessageDto chatMessage, int userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/chat/{userId}", chatMessage);
            return await response.Content.ReadFromJsonAsync<MessageDto>();
        }

        public async Task UnsendMessageAsync(int messageId)
        {
            await _httpClient.DeleteAsync($"/api/chat/message/{messageId}");
        }

        public async Task<MessageDto> UpdateMessageAsync(int messageId, SendMessageDto request)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/chat/message/{messageId}", request);
            return await response.Content.ReadFromJsonAsync<MessageDto>();
        }

        public async Task<List<MessageDto>> GetMessagesByContentAsync(string content)
        {
            return await _httpClient.GetFromJsonAsync<List<MessageDto>>($"/api/chat/message?content={content}");
        }
    }

}
