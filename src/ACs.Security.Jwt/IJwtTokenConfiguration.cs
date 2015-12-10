namespace ACs.Security.Jwt
{
    public class IJwtTokenConfiguration
    {
        public string CertXml { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
}
