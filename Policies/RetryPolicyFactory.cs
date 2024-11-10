using Polly;
using Polly.Retry;

namespace Policies;

public static class RetryPolicyFactory
{
    static RetryPolicyFactory()
    {
        
    }

    public static AsyncPolicy Create<T>(Exception exception)
    {
        return new AsyncPolicy<T>(_ => Task.FromResult(exception));
    }
}