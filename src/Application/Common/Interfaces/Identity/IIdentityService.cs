﻿using StoreDashboard.Blazor.Application.Features.Identity.DTOs;

namespace StoreDashboard.Blazor.Application.Common.Interfaces.Identity;

public interface IIdentityService : IService
{
    Task<string?> GetUserNameAsync(string userId, CancellationToken cancellation = default);
    Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellation = default);
    Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellation = default);
    Task<IDictionary<string, string?>> FetchUsers(string roleName, CancellationToken cancellation = default);
    Task<ApplicationUserDto?> GetApplicationUserDto(string userName, CancellationToken cancellation = default);
    string GetUserName(string userId);
    Task<List<ApplicationUserDto>?> GetUsers(string? tenantId, CancellationToken cancellation = default);
    void RemoveApplicationUserCache(string userName);
}