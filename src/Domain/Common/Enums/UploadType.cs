using System.ComponentModel;

namespace StoreDashboard.Blazor.Domain.Common.Enums;

public enum UploadType : byte
{
    [Description(@"Products")] Product,
    [Description(@"ProfilePictures")] ProfilePicture,
    [Description(@"Documents")] Document,
    [Description(@"Images")] Image,
}