using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.Repositories;
using FluentAssertions;
using GenerationRules;
using GenerationRules.Rules;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public abstract class RepositoryTests<TEntity> : LibraryTestBase where TEntity : class, IEntity, new()
{
    private readonly GenerationRuleBase<TEntity> _generationRule;
    private readonly IRepository<TEntity> _repository;
    protected readonly IUnitOfWork UnitOfWork;

    public RepositoryTests(GenerationRuleBase<TEntity> generationRule)
    {
        _generationRule = generationRule;
        _repository = new Repository<TEntity>(Context);
        UnitOfWork = new UnitOfWork(Context);
    }

    [Fact]
    public async Task SaveAsync()
    {
        var entity = _generationRule.Generate();

        await _repository.SaveAsync(entity);
        await UnitOfWork.CommitAsync();

        var entityFromDb = await Context.FindAsync<TEntity>(entity.Id);
        entityFromDb.Should().Be(entity);
    }

    [Fact]
    public async Task SaveAsync_ShouldCommitCountLikeCountOfSavedEntities()
    {
        var entities = _generationRule.GenerateLazy(5).ToArray();
        var savedEntities = await _repository.SaveAsync(entities);
        var committedEntitiesCount = await UnitOfWork.CommitAsync();
        
        committedEntitiesCount.Should().Be(savedEntities.Count());
    }

    [Fact]
    public async Task GetAsync()
    {
        var entity = _generationRule.Generate();
        await SaveEntitiesAsync(entity);
        
        var entityFromDb = await _repository.GetAsync(entity.Id);
        entityFromDb.Should().Be(entity);
    }

    [Fact]
    public async Task GetAllAsync()
    {
        var entities = _generationRule.GenerateLazy(5).ToArray();
        await SaveEntitiesAsync(entities);
        
        var entitiesFromDb = await _repository.GetAllAsync();
        entitiesFromDb.Should().BeEquivalentTo(entities);
    }
}