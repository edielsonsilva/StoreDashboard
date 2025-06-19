using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Documents.Caching;
using StoreDashboard.Blazor.Domain.Common.Enums;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Documents.Commands.Upload;

public class UploadDocumentCommand : ICacheInvalidatorRequest<Result<int>>
{
    public UploadDocumentCommand(List<UploadRequest> uploadRequests)
    {
            UploadRequests = uploadRequests;
        }
    public List<UploadRequest> UploadRequests { get; set; }
    public IEnumerable<string>? Tags => DocumentCacheKey.Tags;
}

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUploadService _uploadService;

    public UploadDocumentCommandHandler(
        IApplicationDbContext context,
        IUploadService uploadService
    )
    {
            _context = context;
            _uploadService = uploadService;
        }

    public async Task<Result<int>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
            var list = new List<Document>();
            foreach (var uploadRequest in request.UploadRequests)
            {
                var fileName = uploadRequest.FileName;
                var url = await _uploadService.UploadAsync(uploadRequest);
                var document = new Document
                {
                    Title = fileName,
                    URL = url,
                    Status = JobStatus.Queueing,
                    IsPublic = true,
                    DocumentType = DocumentType.Image
                };
                document.AddDomainEvent(new CreatedEvent<Document>(document));
                list.Add(document);
            }

            if (!list.Any()) return await Result<int>.SuccessAsync(0);

            await _context.Documents.AddRangeAsync(list, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }
}