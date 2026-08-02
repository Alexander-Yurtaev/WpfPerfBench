using System.Security;

namespace WpfPerfBench.Core.Helpers;

public static class SecurityHelpers
{
    public static SecureString CreateSecureString(string value)
    {
        var secureString = new SecureString();
        foreach (var c in value)
        {
            secureString.AppendChar(c);
        }

        return secureString;
    }
}