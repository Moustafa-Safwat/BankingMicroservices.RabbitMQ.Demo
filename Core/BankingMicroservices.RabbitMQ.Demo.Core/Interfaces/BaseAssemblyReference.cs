using System.Reflection;

namespace BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

/// <summary>
/// Provides a base class for getting the assembly of a project.
/// </summary>
public abstract class BaseAssemblyReference
{
    /// <summary>
    /// Gets the assembly of the derived project.
    /// </summary>
    public static Assembly Assembly => typeof(BaseAssemblyReference).Assembly;
}
