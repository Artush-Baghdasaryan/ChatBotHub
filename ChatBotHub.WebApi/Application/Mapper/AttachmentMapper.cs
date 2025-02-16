using Application.Models.Requests;
using ChatBotHub.WebApi.Application.Models.Dto;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Application.Mapper;

public static class AttachmentMapper {

    public static Attachment Map(AttachmentCreateModel attachmentModel) {
        return new Attachment {
            FileId = attachmentModel.FileId,
            Description = attachmentModel.Description
        };
    }

    public static AttachmentModel Map(Attachment attachment, FileModelModel fileModel) {
        return new AttachmentModel {
            File = fileModel,
            Description = attachment.Description
        };
    }
}