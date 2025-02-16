using Application.Models.Requests;

namespace ChatBotHub.WebApi.Application.Services;

public interface IIndexService {
    Task IndexAsync(Guid chatBotId, List<AttachmentCreateModel> attachments);
    Task<string> QueryAsync(Guid chatBotId, string query);
}