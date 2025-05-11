using ChatBotHub.Domain.Attachments;
using ChatBotHub.Infrastructure.Common;
using ChatBotHub.Infrastructure.MongoDb;

namespace ChatBotHub.Infrastructure.Repositories;

public class AttachmentRepository : DataRepository<Attachment>, IAttachmentRepository {
    
    public AttachmentRepository(MongoDbDataContext context) : base(context, "attachments") {
    }
}
