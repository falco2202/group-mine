namespace Infrastructure.Authentication
{
    public class JwtSetting
    {
        public const string SectionName = "JwtSetting";
        public string Secret { get; init; } = String.Empty;
        public string Issuer { get; init; } = String.Empty;
        public string Audience { get; init; } = String.Empty;
        public double ExpityMinutes { get; init; }
    }
}
