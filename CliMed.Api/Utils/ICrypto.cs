namespace CliMed.Api.Utils
{
    public interface ICrypto
    {
        ushort HashWorkFactor { get; set; }
        string HashPassword(string plainTextPassword);
        bool IsMatchPassword(string plainTextPassword, string hashedPassword);
    }
}
