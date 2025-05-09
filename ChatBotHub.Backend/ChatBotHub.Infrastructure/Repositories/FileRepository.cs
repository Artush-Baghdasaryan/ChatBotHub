using ChatBotHub.Domain.Files;
using ChatBotHub.Infrastructure.Common;
using ChatBotHub.Infrastructure.MongoDb;
using File = ChatBotHub.Domain.Files.File;

namespace ChatBotHub.Infrastructure.Repositories;

public class FileRepository : DataRepository<File>, IFileRepository {
    public FileRepository(MongoDbDataContext context) : base(context, "files") {
    }
}
