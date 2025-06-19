﻿namespace StoreDashboard.Blazor.Domain.Common.Entities;

public abstract class BaseAuditableSoftDeleteEntity : BaseAuditableEntity, ISoftDelete
{
    public DateTime? Deleted { get; set; }
    public string? DeletedBy { get; set; }
}