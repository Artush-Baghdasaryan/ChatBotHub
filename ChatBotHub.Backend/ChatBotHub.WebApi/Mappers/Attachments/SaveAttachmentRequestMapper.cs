using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.WebApi.Models.Attachments;

namespace ChatBotHub.WebApi.Mappers.Attachments;

public static class SaveAttachmentRequestMapper {
    public static SaveAttachmentRequest Map(SaveAttachmentRequestModel model) {
        return new SaveAttachmentRequest {
            FileId = model.FileId,
            Description = model.Description,
        };
    }
}
