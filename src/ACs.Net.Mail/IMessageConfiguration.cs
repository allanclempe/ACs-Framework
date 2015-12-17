namespace Portal.Core.Infraestrutura
{
    public interface IEmailConfiguration
    {
        string FromAddress { get; }
        string Smtp { get; }
        int Port { get; }
        string UserName { get; }
        string Password { get; }
        bool UseSSL { get; }
        bool Activated { get; }
    }

   
}
