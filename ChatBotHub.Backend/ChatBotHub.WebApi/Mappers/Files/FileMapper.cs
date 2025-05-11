using System.Diagnostics.CodeAnalysis;
using ChatBotHub.WebApi.Mappers.Common;
using ChatBotHub.WebApi.Models.Files;
using File = ChatBotHub.Domain.Files.File;

namespace ChatBotHub.WebApi.Mappers.Files;

public static class FileMapper {
    [return: NotNullIfNotNull(nameof(file))]
    public static FileModel? Map(File? file) {
        if (file is null) {
            return null;
        }

        return new FileModel {
            Id = file.Id,
            FileName = file.FileName,
            FileType = file.FileType,
            Audit = AuditMapper.Map(file.Audit)
        };
    }
}
