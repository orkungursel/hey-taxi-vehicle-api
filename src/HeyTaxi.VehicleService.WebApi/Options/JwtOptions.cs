namespace HeyTaxi.VehicleService.WebApi.Options;

public class JwtOptions
{
    public const string Section = "Jwt";
    public string Issuer { get; init; } = default!;
    public string RsaPemFilePath { get; init; } = "/etc/certs/access-token-public-key.pem";
    public char[] RsaPemFileContents => File.ReadAllText(RsaPemFilePath).ToCharArray();
}