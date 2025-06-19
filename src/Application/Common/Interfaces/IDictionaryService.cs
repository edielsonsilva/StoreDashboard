﻿namespace StoreDashboard.Blazor.Application.Common.Interfaces;

public interface IDictionaryService
{
    Task<IDictionary<string, string>> Fetch(string typeName);
}