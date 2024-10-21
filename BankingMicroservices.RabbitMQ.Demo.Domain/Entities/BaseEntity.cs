﻿using System.ComponentModel.DataAnnotations;
namespace BankingMicroservices.RabbitMQ.Demo.Core.Entities;

/// <summary>
/// Represents the base entity for all entities in the application.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the row version for concurrency control.
    /// </summary>
    /// <remarks>
    /// This property is used as a cancellation token for concurrency updates.
    /// </remarks>
    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
