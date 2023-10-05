namespace eCommerce.Shared.Cores.Extensions;

public static class StringEx
{
    public static string EmptyIfNull(this string text)
    {
        return string.IsNullOrEmpty(text) ? string.Empty : text;
    }
}