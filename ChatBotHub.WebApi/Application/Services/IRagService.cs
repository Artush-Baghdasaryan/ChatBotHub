using ChatBotHub.WebApi.Application.Models.Dto;

namespace ChatBotHub.WebApi.Application.Services;

public interface IRagService {
    Task<string> IndexAsync(Guid chatBotId, List<AttachmentModel> attachments);
    Task<string> QueryAsync(Guid chatBotId, string query);
}
