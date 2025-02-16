using ChatBotHub.WebApi.Application.Models.Dto;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Application.Mapper;

public static class FileModelMapper {
    public static FileModelModel Map(FileModel fileModel) {
        return new FileModelModel {
            FileName = fileModel.FileName,
            FileData = fileModel.FileData,
            FileType = fileModel.FileType,
            UploadDate = fileModel.UploadDate
        };
    }
}