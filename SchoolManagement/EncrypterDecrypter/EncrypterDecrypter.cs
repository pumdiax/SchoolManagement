using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Encrypter
{
    public class EncrypterDecrypter
    {
        private const string MasterKey = "GoToHell57645@afgh3399"; // Replace with your actual master key

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            using (var aes = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(MasterKey.PadRight(32).Substring(0, 32));
                var iv = aes.IV;

                aes.Key = key;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                {
                    var passwordBytes = Encoding.UTF8.GetBytes(password);
                    var encryptedBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                    var result = Convert.ToBase64String(iv) + ":" + Convert.ToBase64String(encryptedBytes);
                    return result;
                }
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                throw new ArgumentNullException(nameof(encryptedPassword));

            var parts = encryptedPassword.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Invalid encrypted password format.");

            var iv = Convert.FromBase64String(parts[0]);
            var encryptedBytes = Convert.FromBase64String(parts[1]);

            using (var aes = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(MasterKey.PadRight(32).Substring(0, 32));

                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
