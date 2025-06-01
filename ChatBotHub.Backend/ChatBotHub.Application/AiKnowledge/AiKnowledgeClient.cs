using System.Text;
using System.Text.Json;
using ChatBotHub.Application.AiKnowledge.Models.Requests;
using ChatBotHub.Application.AiKnowledge.Options;
using Microsoft.Extensions.Options;

namespace ChatBotHub.Application.AiKnowledge;

public class AiKnowledgeClient : IAiKnowledgeClient {
    private readonly AiKnowledgeOptions _options;
    
    public AiKnowledgeClient(IOptions<AiKnowledgeOptions> options) {
        _options = options.Value;
    }
    
    public async Task IndexAttachmentsAsync(List<IndexKnowledgeRequest> requests) {
        using var httpClient = new HttpClient();

        var url = $"{_options.BaseUrl}/index";
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = GetStringContent(requests);
        SetAuthorizationHeader(request);

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveAttachmentIndexAsync(Guid attachmentId) {
        using var httpClient = new HttpClient();

        var url = $"{_options.BaseUrl}/{attachmentId}/remove_index";
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        SetAuthorizationHeader(request);

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> QueryAsync(ExternalQueryKnowledgeRequest request) {
        using var httpClient = new HttpClient();

        var url = $"{_options.BaseUrl}/query";
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Content = GetStringContent(request);
        SetAuthorizationHeader(httpRequest);

        var response = await httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    
    private StringContent GetStringContent(object obj) {
        return new StringContent(
            JsonSerializer.Serialize(obj),
            Encoding.UTF8,
            "application/json"
        );
    }

    private void SetAuthorizationHeader(HttpRequestMessage request) {
        request.Headers.Add("Authorization", $"Bearer {_options.ApiKey}");
    }
}
