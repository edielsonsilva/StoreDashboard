﻿using StoreDashboard.Blazor.Domain.Common.Entities;
using StoreDashboard.Blazor.Domain.Common.Enums;
using StoreDashboard.Blazor.Domain.Identity;

namespace StoreDashboard.Blazor.Domain.Entities;

public class Document : BaseAuditableEntity, IMayHaveTenant, IAuditTrial
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public JobStatus Status { get; set; } = default!;
    public string? Content { get; set; }
    public bool IsPublic { get; set; }
    public string? URL { get; set; }
    public DocumentType DocumentType { get; set; } = default!;
    public virtual Tenant? Tenant { get; set; }
    public string? TenantId { get; set; }

    public virtual ApplicationUser? CreatedByUser { get;set;}
    public virtual ApplicationUser? LastModifiedByUser { get; set; }
}

public enum DocumentType
{
    Document,
    Excel,
    Image,
    PDF,
    Others
}