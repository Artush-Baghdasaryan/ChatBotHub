using System.Text;
using System.Text.Json;
using ChatBotHub.WebApi.Application.Models.Dto;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class RagService : IRagService {
    private readonly RagServiceSettings _ragServiceSettings;
    private readonly HttpClient _httpClient;

    public RagService(IOptions<RagServiceSettings> ragServiceSettings) {
        _ragServiceSettings = ragServiceSettings.Value;
        _httpClient = new HttpClient {
            Timeout = TimeSpan.FromMinutes(10) // Set timeout to 10 minutes
        };
    }

    public async Task<string> IndexAsync(Guid chatBotId, List<AttachmentModel> attachments) {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_ragServiceSettings.BaseUrl}/{chatBotId}/index");
        request.Content = new StringContent(JsonSerializer.Serialize(attachments), Encoding.UTF8, "application/json");
        SetAuthorizationHeader(request);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;
    }

    public async Task<string> QueryAsync(Guid chatBotId, string query) {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_ragServiceSettings.BaseUrl}/{chatBotId}/query?query={query}");
        SetAuthorizationHeader(request);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;
    }

    private void SetAuthorizationHeader(HttpRequestMessage request) {
        request.Headers.Add("Authorization", $"Bearer {_ragServiceSettings.ApiKey}");
    }
}
