﻿namespace TUnit.Assertions.AssertConditions;

public abstract class ExpectedValueAssertCondition<TActual, TExpected>(TExpected? expected) : BaseAssertCondition<TActual>
{
    private readonly List<(Func<TActual?, TActual?> ActualTransformation,
        Func<TExpected?, TExpected?> ExpectedTransformation)> _transformations = [];

    private readonly List<Func<TActual?, TExpected?, AssertionDecision>> _customComparers = [];

    public TExpected? ExpectedValue { get; } = expected;

    public void WithTransform(Func<TActual?, TActual?> actualTransformation,
        Func<TExpected?, TExpected?> expectedTransformation)
    {
        _transformations.Add((actualTransformation, expectedTransformation));
    }

    public void WithComparer(Func<TActual?, TExpected?, AssertionDecision> comparer)
    {
        _customComparers.Add(comparer);
    }

    protected override ValueTask<AssertionResult> GetResult(
        TActual? actualValue, Exception? exception,
        AssertionMetadata assertionMetadata
    )
    {
        var expected = ExpectedValue;

        foreach (var (actualTransformation, expectedTransformation) in _transformations)
        {
            actualValue = actualTransformation(actualValue);
            expected = expectedTransformation(expected);
        }

        foreach (var result in _customComparers.Select(customComparer => customComparer(actualValue, expected)))
        {
            switch (result)
            {
                case AssertionDecision.PassDecision:
                    return AssertionResult.Passed;
                case AssertionDecision.FailDecision failDecision:
                    return FailWithMessage(failDecision.Message);
            }
        }

        if (exception is not null)
        {
            return FailWithMessage($"An exception was thrown during the assertion: {exception}");
        }

        return GetResult(actualValue, expected);
    }

    protected abstract ValueTask<AssertionResult> GetResult(TActual? actualValue, TExpected? expectedValue);
}
