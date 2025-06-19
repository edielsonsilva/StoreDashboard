using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Documents.Caching;
using StoreDashboard.Blazor.Application.Features.Documents.DTOs;
using StoreDashboard.Blazor.Domain.Common.Enums;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Documents.Commands.AddEdit;

public class AddEditDocumentCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")] public int Id { get; set; }
    [Description("Title")] public string? Title { get; set; }
    [Description("Description")] public string? Description { get; set; }
    [Description("Is Public")] public bool IsPublic { get; set; }
    [Description("URL")] public string? URL { get; set; }
    [Description("Document Type")] public DocumentType DocumentType { get; set; } = DocumentType.Document;
    [Description("Tenant Id")] public string? TenantId { get; set; }
    [Description("Tenant Name")] public string? TenantName { get; set; }
    [Description("Status")] public JobStatus Status { get; set; } = JobStatus.NotStart;
    [Description("Content")] public string? Content { get; set; }
    public UploadRequest? UploadRequest { get; set; }
    public IEnumerable<string>? Tags => DocumentCacheKey.Tags;
    private class Mapping : Profile
    {
        public Mapping()
        {
                CreateMap<DocumentDto, AddEditDocumentCommand>(MemberList.None);
                CreateMap<AddEditDocumentCommand, Document>(MemberList.None);
            }
    }
}

public class AddEditDocumentCommandHandler : IRequestHandler<AddEditDocumentCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IUploadService _uploadService;

    public AddEditDocumentCommandHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IUploadService uploadService
    )
    {
            _mapper = mapper;
            _context = context;
            _uploadService = uploadService;
        }

    public async Task<Result<int>> Handle(AddEditDocumentCommand request, CancellationToken cancellationToken)
    {
            if (request.Id > 0)
            {
                var document = await _context.Documents.FindAsync(request.Id, cancellationToken);
                if (document == null)
                {
                    return await Result<int>.FailureAsync($"Document with id: [{request.Id}] not found.");
                }
                document.AddDomainEvent(new UpdatedEvent<Document>(document));
                if (request.UploadRequest != null) document.URL = await _uploadService.UploadAsync(request.UploadRequest);
                document.Title = request.Title;
                document.Description = request.Description;
                document.IsPublic = request.IsPublic;
                document.DocumentType = request.DocumentType;
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(document.Id);
            }
            else
            {
                var document = _mapper.Map<Document>(request);
                if (request.UploadRequest != null) document.URL = await _uploadService.UploadAsync(request.UploadRequest);
                document.AddDomainEvent(new CreatedEvent<Document>(document));
                _context.Documents.Add(document);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(document.Id);
            }
        }
}