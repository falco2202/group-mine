namespace Infrastructure.Authentication
{
    public class JwtSetting
    {
        public string Secret { get; init; } = String.Empty;
        public string Issuer { get; init; } = String.Empty;
        public string Audience { get; init; } = String.Empty;
        public string ExpirationTimeInMinutes { get; init; } = String.Empty;
    }
}
