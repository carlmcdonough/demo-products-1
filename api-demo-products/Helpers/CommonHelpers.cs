using System.Text;

namespace api_demo_products.Helpers
{
    public class CommonHelpers
    {
        public static string GetResourcesLocation(string resourceId, HttpContext httpContext)
        {
            var builderForUri = new StringBuilder(string.Empty);

            builderForUri.Append($"{httpContext?.Request?.Scheme ?? "http"}" + "://");
            builderForUri.Append($"{httpContext?.Request?.Host ?? new HostString(string.Empty)}");
            builderForUri.Append($"{httpContext?.Request?.Path ?? string.Empty}");

            var uri = builderForUri.ToString().TrimEnd(new[] { '/' });
            uri += $"/{resourceId ?? string.Empty}";

            return uri;
        }
    }
}
