using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {

        char[] e_letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public string Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();

            //Expandable string 
            StringBuilder ki = new StringBuilder();
            cipherText = cipherText.ToLower();
            int flag = 1;

            for (int i = 0; i < plainText.Length; i++)
            {

                ki.Append(e_letters[((Array.IndexOf(e_letters, cipherText[i]) - Array.IndexOf(e_letters, plainText[i])) + 26) % 26]);
            }

            String key = ki[0].ToString();


            while (true)
            {
                if (Encrypt(plainText, key.ToString()).ToString().Equals(cipherText))
                {

                    break;
                }
                else
                {
                    key += ki[flag];
                    flag++;
                }
            }

            return key.ToString();
        }

        public string Decrypt(string cipherText, string key)
        {
            //throw new NotImplementedException();

            string ki = key.ToUpper();
            int kl = ki.Length;
            string ct = cipherText.ToUpper();
            int cl = ct.Length;
            string plaintxt = "";
            for (int i = 0; i < cl; i++)
            {
                if (cl == kl)
                    break;
                ki += ki[i];
            }


            for (int j = 0; j < ki.Length && j < cl; j++)
            {
                int res = (ct[j] - ki[j] + 26) % 26;
                res += 'A';
                plaintxt += (char)(res);
            }
            return plaintxt;
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