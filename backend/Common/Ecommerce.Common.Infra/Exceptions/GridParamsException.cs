namespace Ecommerce.Common.Infra.Exceptions;

/// <summary>
/// Exception thrown for errors related to GridParams.
/// </summary>
public class GridParamsException(string message) : Exception(message)
{
}
