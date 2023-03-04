using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {


        public string Analyse(string plainText, string cipherText)
        {



            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            key = key.ToUpper();

            if (key.Length != 26 || key.Distinct().Count() != 26)
            {
                throw new ArgumentException("The key must contain exactly 26 unique letters.");
            }

            var map = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                map[key[i]] = alphabetc[i];
            }
            var decryptedText = new StringBuilder();
            foreach (char c in cipherText.ToUpper())
            {
                if (map.ContainsKey(c))
                {
                    decryptedText.Append(map[c]);
                }
                else
                {
                    decryptedText.Append(c);
                }
            }

            return decryptedText.ToString();
        }

        private readonly string alphabetc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";






        public string Encrypt(string plainText, string key)
        {
            key = key.ToUpper();

            if (key.Length != 26 || key.Distinct().Count() != 26)
            {
                throw new ArgumentException("The key must contain exactly 26 unique letters.");
            }

            var map = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                map[alphabetc[i]] = key[i];
            }


            var encryptedText = new StringBuilder();
            foreach (char c in plainText.ToUpper())
            {
                if (map.ContainsKey(c))
                {
                    encryptedText.Append(map[c]);
                }
                else
                {
                    encryptedText.Append(c);
                }
            }

            return encryptedText.ToString();
        }





        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            throw new NotImplementedException();
        }
    }
}