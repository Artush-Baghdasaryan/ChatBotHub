using ChatBotHub.Application.ChatBots.Exceptions;
using ChatBotHub.Domain.ChatBots;

namespace ChatBotHub.Application.ChatBots;

public class ChatBotQueryService : IChatBotQueryService {
    private readonly IChatBotRepository _repository;

    public ChatBotQueryService(IChatBotRepository chatBotRepository) {
        _repository = chatBotRepository;
    }
    
    public Task<ChatBot?> GetByIdAsync(Guid id) {
        return _repository.GetByIdAsync(id);
    }

    public async Task<ChatBot> RequireAsync(Guid id) {
        return await _repository.GetByIdAsync(id) ??
            throw new ChatBotIsNotFoundException(id);
    }

    public Task<List<ChatBot>> GetAllAsync() {
        return _repository.GetAllAsync();
    }
}
