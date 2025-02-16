using ChatBotHub.WebApi.Infrastructure.Context;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Infrastructure.Repositories;


public class FileModelRepository : BaseRepository<FileModel> {
    public FileModelRepository(DataContext context) : base(context, "fileModels") {
    }
}
