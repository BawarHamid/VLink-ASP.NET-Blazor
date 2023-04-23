using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Vikarlink.WebApp.StateProvider
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
       
        private readonly HttpClient _httpClient;


        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsStringAsync("token");

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrWhiteSpace(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "JWT");
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }



    }
}
