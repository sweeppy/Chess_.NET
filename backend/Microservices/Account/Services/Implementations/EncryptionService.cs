using System.Text;
using Account.Services.Interfaces;

namespace Account.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionService(IConfiguration configuration)
        {
            _key = Encoding.UTF8.GetBytes(configuration["EncryptionKey"]);
            _iv = Encoding.UTF8.GetBytes(configuration["EncryptionIV"]);
        }

        public string Decrypt(string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText)
        {
            throw new NotImplementedException();
        }
    }
}