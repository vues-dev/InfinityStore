using Microsoft.AspNetCore.Components.Forms;

namespace InfinityStoreAdmin.BlazorApp.Shared.Utilities;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _environment;

    public FileUploadService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    public async Task<UploadFileResponse> UploadFileAsync(IBrowserFile file, int maxFileSize, string[] allowedExtensions)
    {
        var uploadDirectory = Path.Combine(_environment.WebRootPath, "images");

        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        if (file.Size > maxFileSize)
        {
            return new UploadFileResponse
            {
                StatusCode = 0,
                StatusMessage = $"File: {file.Name} exceeds the maximum allowed file size."
            };
        }

        var fileExtension = Path.GetExtension(file.Name);

        if (!allowedExtensions.Contains(fileExtension))
        {
            return new UploadFileResponse
            {
                StatusCode = 0,
                StatusMessage = $"File: {file.Name}, File type not allowed"
            };
        }

        var fileName = $"{file.Name}{fileExtension}";
        var path = Path.Combine(uploadDirectory, fileName);
        await using var fs = new FileStream(path, FileMode.Create);
        await file.OpenReadStream(maxFileSize).CopyToAsync(fs);

        var imageUrl = Path.Combine("images", fileName);

        return new UploadFileResponse
        {
            StatusCode = 1,
            StatusMessage = $"File: {file.Name} uploaded successfully",
            ImageUrl = imageUrl
        };
    }
}