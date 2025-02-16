using ChatBotHub.WebApi.Infrastructure.Context;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Infrastructure.Repositories;

public class AttachmentRepository : BaseRepository<Attachment> {
    public AttachmentRepository(DataContext context) : base(context, "attachments") {
    }
}

