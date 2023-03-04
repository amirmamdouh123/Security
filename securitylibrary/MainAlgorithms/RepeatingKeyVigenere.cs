using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {

            Console.WriteLine(plainText);
            int diff = plainText.Length - key.Length;
            for (int i = 0; i < diff; i += 1)
            {
                key += key[i];
            }
            Console.WriteLine(key);
            string encripted = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                encripted += (char)((((plainText[i] - 97) + (key[i] - 97)) % 26) + 97);
            }
            Console.WriteLine(encripted);
            return encripted;

        }
    }
}