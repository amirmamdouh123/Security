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
            // throw new NotImplementedException();
            int PTlenght, CTlenght;
            string key = "";
            string letters = "abcdefghijklmnopqrstuvwxyz";

            SortedDictionary<char, char> keyDec = new SortedDictionary<char, char>();
            Dictionary<char, bool> cipherDec = new Dictionary<char, bool>();

            PTlenght = plainText.Length;
            CTlenght = cipherText.Length;
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();

            //هنحط البلان مع السيفر
            for (int i = 0; i < PTlenght; i++)
            {
                if (!keyDec.ContainsKey(plainText[i]))
                {
                    keyDec.Add(plainText[i], cipherText[i]);
                    cipherDec.Add(cipherText[i], true);
                }
            }


            //بكمل الفاضى فى الديكشنرى
            //بملاها ب استرنج من (a to z)
            if (keyDec.Count != 26)
            {   //فور لوب بتلف على ال key
                for (int j = 0; j < 26; j++)
                {
                    if (!keyDec.ContainsKey(letters[j]))
                    {   //فور لوب على السيفر علشان اعمل ادد ب الترتيب
                        for (int k = 0; k < 26; k++)
                        {
                            if (!cipherDec.ContainsKey(letters[k]))
                            {
                                keyDec.Add(letters[j], letters[k]);
                                cipherDec.Add(letters[k], true);
                                k = 26;
                            }
                        }
                    }
                }
            }

            foreach (var items in keyDec)
            {
                key += items.Value;
            }
            return key;

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
            //throw new NotImplementedException();
            // histogram
            string alphabet_freq = "etaoinsrhldcumfpgwybvkxjqz";
            Dictionary<char, int> freq = new Dictionary<char, int>();
            Dictionary<char, char> result = new Dictionary<char, char>();
            cipher = cipher.ToLower();
            string p_text = "";
            int cont = 0;

            for (int i = 0; i < cipher.Length; i++)
            {
                if (freq.ContainsKey(cipher[i]))
                {
                    freq[cipher[i]]++;
                }
                else
                {
                    freq.Add(cipher[i], 0);
                }
            }

            freq = freq.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in freq)
            {
                result.Add(item.Key, alphabet_freq[cont]);
                cont++;
            }

            int j = 0;
            while (j < cipher.Length)
            {
                p_text = p_text + result[cipher[j]];

                j++;
            }
            return p_text;
        }
    }
}