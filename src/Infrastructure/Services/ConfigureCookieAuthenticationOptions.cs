﻿using Microsoft.AspNetCore.Authentication.Cookies;

namespace StoreDashboard.Blazor.Infrastructure.Services;
#nullable disable
public class ConfigureCookieAuthenticationOptions
    : IPostConfigureOptions<CookieAuthenticationOptions>
{
    private readonly ITicketStore _ticketStore;

    public ConfigureCookieAuthenticationOptions(ITicketStore ticketStore)
    {
        _ticketStore = ticketStore;
    }

    public void PostConfigure(string name,
        CookieAuthenticationOptions options)
    {
        options.SessionStore = _ticketStore;
    }
}