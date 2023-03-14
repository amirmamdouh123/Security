using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {

        char[] e_letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public string Analyse(string plainText, string cipherText)
        {
            string txt = "";
            string ki = "";
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();

            for (int i = 0; i < cipherText.Length; i++)
            {
                txt += e_letters[(Array.IndexOf(e_letters, cipherText[i]) - Array.IndexOf(e_letters, plainText[i]) + 26) % 26];
            }

            int ki_L = txt.Length;

            for (int i = 0; i < ki_L; i++)
            {
                if (cipherText != Encrypt(plainText, ki))
                {
                    ki += txt[i];

                }
                else
                {
                    break;

                }

            }

            return ki;

        }

        public string Decrypt(string cipherText, string key)
        {
            //throw new NotImplementedException();
            string plaintxt = "";
            string ct = cipherText.ToUpper();
            int len = ct.Length;
            string ki = key.ToUpper();

            for (int i = 0; i < len; i++)
            {

                int res = (ct[i] - ki[i] + 26) % 26;
                res += 'A';
                ki += (char)(res);
            }

            for (int j = 0; j < len; j++)
            {
                int res = (ct[j] - ki[j] + 26) % 26;
                res += 'A';
                plaintxt += (char)(res);
            }
            return plaintxt;
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            Console.WriteLine(plainText);
            int diff = plainText.Length - key.Length;
            for (int i = 0; i < diff; i += 1)
            {
                key += plainText[i];
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