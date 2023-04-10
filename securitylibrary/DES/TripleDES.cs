using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class TripleDES : ICryptographicTechnique<string, List<string>>
    {
        DES des = new DES();
        public string Decrypt(string cipherText, List<string> key)
        {
           
            return des.Decrypt(des.Encrypt(des.Decrypt(cipherText, key[0]), key[1]), key[0]);

        }

        public string Encrypt(string plainText, List<string> key)
        {
            
            string x = des.Encrypt(plainText, key[0]);
            string y = des.Decrypt(x, key[1]);
            string cipher = des.Encrypt(y, key[0]);
            return cipher;
        }

        public List<string> Analyse(string plainText, string cipherText)
        {
            throw new NotSupportedException();
        }

    }
}
