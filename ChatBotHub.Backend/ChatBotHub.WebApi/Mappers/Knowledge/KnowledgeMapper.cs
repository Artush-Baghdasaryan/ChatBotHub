using ChatBotHub.Application.AiKnowledge.Models.Requests;
using ChatBotHub.WebApi.Models.Knowledge;

namespace ChatBotHub.WebApi.Mappers.Knowledge;

public class KnowledgeMapper {
    public static QueryKnowledgeRequest Map(QueryKnowledgeRequestModel request) {
        return new QueryKnowledgeRequest {
            Query = request.Query,
            SessionId = request.SessionId
        };
    }
}
