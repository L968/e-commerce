using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Domain.Errors;

public class DomainException : Exception
{
    public IEnumerable<string> Errors { get; }
    public bool IsNotFound { get; }

    public DomainException(string message, bool isNotFound = false) : base(message)
    {
        Errors = [message];
        IsNotFound = isNotFound;
    }

    public DomainException(IEnumerable<string> messages, bool isNotFound = false) : base(string.Join("; ", messages))
    {
        Errors = messages;
        IsNotFound = isNotFound;
    }

    public static void ThrowIfNull<T>([NotNull] T? obj, int id) where T : class
    {
        if (obj is null)
        {
            throw new DomainException(
                message: DomainErrors.NotFound(typeof(T).Name, id),
                isNotFound: true
            );
        }
    }

    public static void ThrowIfNull<T>([NotNull] T? obj, Guid id) where T : class
    {
        if (obj is null)
        {
            throw new DomainException(
                message: DomainErrors.NotFound(typeof(T).Name, id),
                isNotFound: true
            );
        }
    }

    public static void ThrowIfNull<T>([NotNull] T? obj, string message, params object[] args) where T : class
    {
        if (obj is null)
        {
            var formattedMessage = string.Format(message, args);

            throw new DomainException(
                message: formattedMessage,
                isNotFound: true
            );
        }
    }
}
