namespace ACs.Net.Mail
{
    public interface ISmptConfiguration
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
