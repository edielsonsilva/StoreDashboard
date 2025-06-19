namespace StoreDashboard.Blazor.Application.Common.Interfaces;

public interface IDocumentOcrJob
{
    void Do(int id);
    Task Recognition(int id, CancellationToken cancellationToken);
}