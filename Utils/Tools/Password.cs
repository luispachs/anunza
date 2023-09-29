using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography;
using System.Text;
namespace TablonAnuncios.Utils.Tools
{
    public class Password
    {   
        
        public static string encode(string password)
        {
            byte[] tempData = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] tmpHas =SHA1.Create().ComputeHash(tempData);
            return BitConverter.ToString(tmpHas).Replace("-","").ToLower();
        }
        public static bool compare(string password,string hash)
        {
            if (password == null || hash == null) return false;
            var hashedPassword = encode(password);
            if (hashedPassword != hash)
            {
                return false;
            }

            return true;
        }
    }
}
