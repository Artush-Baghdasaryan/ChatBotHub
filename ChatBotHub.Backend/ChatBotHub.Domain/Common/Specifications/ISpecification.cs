using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Common.Specifications;

public interface ISpecification<TEntity> where TEntity : Entity {
    SpecificationResult IsSatisfiedBy(TEntity entity);
}
