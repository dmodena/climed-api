namespace CliMed.Api.Utils
{
    public class Crypto : ICrypto
    {
        public const ushort StandardHashWorkFactor = 10;
        private ushort hashWorkFactor = StandardHashWorkFactor;

        public ushort HashWorkFactor
        {
            get => hashWorkFactor;
            set => hashWorkFactor = value == 0 ? StandardHashWorkFactor : value;
        }

        public string HashPassword(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword, BCrypt.Net.BCrypt.GenerateSalt(HashWorkFactor));
        }

        public bool IsMatchPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }
    }
}
