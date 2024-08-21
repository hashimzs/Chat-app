using Blazored.LocalStorage;
using library.Dtos;
using Library.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Library.Services
{
    public class AuthService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly NavigationManager navigationManager;
        private UserDto? user = null;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.navigationManager = navigationManager;
        }

        public async Task Register(RegisterReqDto registerReqDto)
        {
            var response = await this.httpClient.PostAsJsonAsync<RegisterReqDto>("auth/users/register", registerReqDto);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }

        public async Task<UserDto> getUserInfo()
        {
            if (user == null)
                user = await this.httpClient.GetFromJsonAsync<UserDto>("auth/users");

            return user;
        }

        public async Task Login(LoginReqDto loginReqDto)
        {
            var response = await this.httpClient.PostAsJsonAsync<LoginReqDto>("auth/users/login", loginReqDto);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            var token = (await response.Content.ReadFromJsonAsync<LoginResponseDto>()).token;

            await this.localStorageService.SetItemAsync<string>("token", token);

            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }


        public async Task Logout()
        {
            await this.localStorageService.RemoveItemAsync("token");

            navigationManager.NavigateTo("login");

        }

        public async Task<bool> IsLoggedIn()
        {
            var token = await this.localStorageService.GetItemAsync<string>("token");

            if (string.IsNullOrEmpty(token))
                return false;
            return true;
        }

        public async Task AddDefaultReqHeaders()
        {
            var token = await this.localStorageService.GetItemAsync<string>("token");

            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }
}
