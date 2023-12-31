namespace ePOS.Shared.ValueObjects;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    
    public MailSetting MailSetting { get; set; } = default!;
    
    public JwtTokenSetting JwtTokenSetting { get; set; } = default!;
}

public class ConnectionStrings
{
    public string SQLServerConnection { get; set; } = default!;
}

public class MailSetting
{
    public string Host { get; set; } = default!;
    
    public int Port { get; set; }
    
    public string DisplayName { get; set; } = default!;
    
    public string Mail { get; set; } = default!;
    
    public string Password { get; set; } = default!;
}

public class JwtTokenSetting
{
    public string ServerSecretKey { get; set; } = default!;
    
    public int AccessTokenExpirationMinutes { get; set; }
    
    public int RefreshTokenExpirationMinutes { get; set; }
}