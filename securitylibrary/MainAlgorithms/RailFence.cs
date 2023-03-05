using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
           plainText= plainText.ToLower();
            cipherText = cipherText.ToLower();
            Console.WriteLine(cipherText);
            Console.WriteLine(plainText);

            int key = 1;
            //throw new NotImplementedException();
            for (int i = 1; i < plainText.Length; i++)
            {
                for (int j = 1; j < plainText.Length; j++) {
                    
                    if (cipherText[i] == plainText[j] && cipherText[i+1] ==plainText[j+key]) {
                        return key;
                    }
                    key++;
                }
            }
            return 0;
        }

        public string Decrypt(string cipherText, int key)
        {
           
            return "";

        }

        public string Encrypt(string plainText, int key)
        {

            
            string cipherText = "";
            decimal count = plainText.Length;
            int depth =(int) Math.Ceiling(count / key); //error
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < depth; j++)//error
                {
                    try
                    {
                        cipherText+= plainText[(j*key)+i];
                    }
                    catch (Exception e) { }
                }
            }
            return cipherText;
        }
    }
}