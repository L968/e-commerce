using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Domain.Errors;

public class DomainException : Exception
{
    public IEnumerable<string> Errors { get; }

    public DomainException(string message) : base(message)
    {
        Errors = [message];
    }

    public DomainException(IEnumerable<string> messages) : base(string.Join("; ", messages))
    {
        Errors = messages;
    }

    public static void ThrowIfNull<T>([NotNull] T? obj, int id) where T : class
    {
        if (obj is null)
        {
            throw new DomainException(DomainErrors.NotFound(nameof(obj), id));
        }
    }

    public static void ThrowIfNull<T>([NotNull] T? obj, Guid id) where T : class
    {
        if (obj is null)
        {
            throw new DomainException(DomainErrors.NotFound(nameof(obj), id));
        }
    }

    public static void ThrowIfNull<T>([NotNull] T? obj, string message) where T : class
    {
        if (obj is null)
        {
            throw new DomainException(message);
        }
    }
}
