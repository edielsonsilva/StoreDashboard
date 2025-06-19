﻿namespace StoreDashboard.Blazor.Application.Features.Documents.Commands.Upload;

public class UploadDocumentCommandValidator : AbstractValidator<UploadDocumentCommand>
{
    public UploadDocumentCommandValidator()
    {
            RuleFor(v => v.UploadRequests).NotNull().NotEmpty();
        }
}