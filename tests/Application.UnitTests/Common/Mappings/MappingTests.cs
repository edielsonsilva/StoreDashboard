using System;
using System.Reflection;
using System.Runtime.Serialization;
using AutoMapper;
using StoreDashboard.Blazor.Application.Features.Contacts.DTOs;
using NUnit.Framework;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Features.AuditTrails.DTOs;
using StoreDashboard.Blazor.Application.Features.Documents.DTOs;
using StoreDashboard.Blazor.Application.Features.Identity.DTOs;
using StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;
using StoreDashboard.Blazor.Application.Features.Products.DTOs;
using StoreDashboard.Blazor.Application.Features.SystemLogs.DTOs;
using StoreDashboard.Blazor.Application.Features.Tenants.DTOs;
using StoreDashboard.Blazor.Domain.Entities;
using StoreDashboard.Blazor.Domain.Identity;

namespace StoreDashboard.Blazor.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));
        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Document), typeof(DocumentDto), true)]
    [TestCase(typeof(Tenant), typeof(TenantDto), true)]
    [TestCase(typeof(Product), typeof(ProductDto), true)]
    [TestCase(typeof(Contact), typeof(ContactDto), true)]
    [TestCase(typeof(PicklistSet), typeof(PicklistSetDto), true)]
    [TestCase(typeof(ApplicationUser), typeof(ApplicationUserDto), false)]
    [TestCase(typeof(ApplicationRole), typeof(ApplicationRoleDto), false)]
    [TestCase(typeof(SystemLog), typeof(SystemLogDto), false)]
    [TestCase(typeof(AuditTrail), typeof(AuditTrailDto), false)]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination, bool inverseMap = false)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);

        if (inverseMap)
        {
            ShouldSupportMappingFromSourceToDestination(destination, source, false);
        }
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type);

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}