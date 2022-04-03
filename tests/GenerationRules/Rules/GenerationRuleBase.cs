using Bogus;

namespace GenerationRules.Rules;

public abstract class GenerationRuleBase<T> : Faker<T> where T : class
{
    protected GenerationRuleBase()
    {
        Locale = "en";
    }
}