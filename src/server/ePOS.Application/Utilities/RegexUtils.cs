using System.Text.RegularExpressions;

namespace ePOS.Shared.Utils;

public static class RegexUtils
{
    public static readonly Regex EmailRegex = new(@"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$");
}