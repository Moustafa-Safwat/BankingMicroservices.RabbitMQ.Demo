namespace BankingMicroservices.RabbitMQ.Demo.Core.Shared;

/// <summary>
/// Initializes a new instance of the <see cref="Error"/> class with the specified error code and message.
/// </summary>
/// <param name="code">The error code.</param>
/// <param name="message">The error message.</param>
public class Error(string code, string message) : IEquatable<Error>
{

    #region Properties
    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string Code => code;

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message => message;
    #endregion

    #region Static Properties
    /// <summary>
    /// Represents an empty error.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// Represents an error when the specified result value is null.
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    #endregion

    #region Methods
    /// <summary>
    /// Determines whether the specified <see cref="Error"/> is equal to the current <see cref="Error"/>.
    /// </summary>
    /// <param name="other">The <see cref="Error"/> to compare with the current <see cref="Error"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Error"/> is equal to the current <see cref="Error"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }
        return Code == other.Code && Message == other.Message;
    }
    /// <summary>
    /// Determines whether the specified <see cref="Error"/> is equal to the current <see cref="Error"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="Error"/>.</param>
    /// <returns><c>true</c> if the specified object is equal to the current <see cref="Error"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj) => obj is Error error && Equals(error);
    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current <see cref="Error"/>.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }
    /// <summary>
    /// Returns a string that represents the current <see cref="Error"/>.
    /// </summary>
    /// <returns>A string that represents the current <see cref="Error"/>.</returns>
    public override string ToString() => Code;
    #endregion

    #region Operator Methods

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a string by returning the error code.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> to convert.</param>
    /// <returns>The error code as a string.</returns>
    public static implicit operator string(Error error) => error.Code;

    /// <summary>
    /// Determines whether two <see cref="Error"/> objects are equal by comparing their error codes and messages.
    /// </summary>
    /// <param name="left">The first <see cref="Error"/> to compare.</param>
    /// <param name="right">The second <see cref="Error"/> to compare.</param>
    /// <returns><c>true</c> if the error codes and messages are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Error? left, Error? right)
    {
        if (left is null && right is null)
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Error"/> objects are not equal by comparing their error codes and messages.
    /// </summary>
    /// <param name="left">The first <see cref="Error"/> to compare.</param>
    /// <param name="right">The second <see cref="Error"/> to compare.</param>
    /// <returns><c>true</c> if the error codes and messages are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Error? left, Error? right) => !(left == right);
    #endregion
}
