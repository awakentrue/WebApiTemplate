using Domain.Entities;

namespace GenerationRules.Rules;

public class BookGenerationRule : GenerationRuleBase<Book>
{
    public BookGenerationRule()
    {
        RuleFor(x => x.Genre, f => f.Random.String());
        RuleFor(x => x.Title, f => f.Random.String());
    }
}