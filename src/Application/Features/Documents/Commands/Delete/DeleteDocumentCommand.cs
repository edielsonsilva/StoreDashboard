﻿using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Documents.Caching;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Documents.Commands.Delete;

public class DeleteDocumentCommand : ICacheInvalidatorRequest<Result>
{
    public DeleteDocumentCommand(int[] id)
    {
            Id = id;
        }

    public int[] Id { get; set; }
    public IEnumerable<string>? Tags => DocumentCacheKey.Tags;
}

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Result>

{
    private readonly IApplicationDbContext _context;

    public DeleteDocumentCommandHandler(
        IApplicationDbContext context
    )
    {
            _context = context;
        }

    public async Task<Result> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
            var items = await _context.Documents.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                item.AddDomainEvent(new DeletedEvent<Document>(item));
                _context.Documents.Remove(item);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return await Result.SuccessAsync();
        }
}