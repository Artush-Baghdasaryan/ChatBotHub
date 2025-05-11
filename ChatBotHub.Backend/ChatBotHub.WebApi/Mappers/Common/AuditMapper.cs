using ChatBotHub.Domain.Common.Models;
using ChatBotHub.WebApi.Models.Common;

namespace ChatBotHub.WebApi.Mappers.Common;

public static class AuditMapper {
    public static AuditModel Map(Audit audit) {
        return new AuditModel {
            CreatedOn = audit.CreatedOn,
            ModifiedOn = audit.ModifiedOn,
        };
    }
}