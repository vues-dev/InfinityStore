namespace InfinityStoreAdmin.Api.Shared.Extensions
{
    public static class ValidationExtensions
    {
        public static bool BeAValidUrl(string imageUrl)
        {
            return Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
