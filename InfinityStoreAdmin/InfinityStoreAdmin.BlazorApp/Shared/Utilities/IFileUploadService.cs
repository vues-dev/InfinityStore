using Microsoft.AspNetCore.Components.Forms;
using static InfinityStoreAdmin.BlazorApp.Shared.Utilities.FileUploadService;

namespace InfinityStoreAdmin.BlazorApp.Shared.Utilities
{
    public interface IFileUploadService
    {
        Task<UploadFileResponse> UploadFileAsync(IBrowserFile file, int maxFileSize, string[] allowedExtensions);
    }
}
