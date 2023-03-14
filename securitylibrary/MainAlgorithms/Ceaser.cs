using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            string encripted = "";
            foreach (char x in plainText) {
                int num = ((int)x-97+key)%26;
                char encripted_letter =(char)(num+97);
                encripted += encripted_letter;
            }
            Console.WriteLine(encripted);
            return encripted;
        }

        public string Decrypt(string cipherText, int key)
        {
            cipherText = cipherText.ToLower();
            string decripted = "";
            foreach (char x in cipherText)
            {
                int num = (((int)x - 97) - key) % 26;
                if (num < 0) num += 26;
                char decripted_letter = (char)(num + 97);
                decripted += decripted_letter;
            }
            Console.WriteLine(decripted);
            return decripted;
        }

        public int Analyse(string plainText, string cipherText)
        {

            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            Console.WriteLine((int)plainText[0]);
            Console.WriteLine((int)cipherText[0]);
            int x1 =((int) plainText[0]);
            int x2 = ((int)cipherText[0]);
            int key=(x2 - x1);
            if (key < 0)
                key += 26;
            Console.WriteLine((x2 - x1));
            return key;
        }
    }
}
