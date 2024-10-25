using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using System.Reflection;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application
{
    /// <summary>
    /// Provides a reference to the assembly containing the Banking Application.
    /// Inherits from <see cref="BaseAssemblyReference"/>.
    /// </summary>
    public class AssemblyReference : BaseAssemblyReference
    {
        /// <summary>
        /// Gets the assembly of the <see cref="AssemblyReference"/> type.
        /// </summary>
        public new readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
