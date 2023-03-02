using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {   
          
            Encrypt(cipherText, key);

            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            key = "playfairexample";
            string filtered = "";
            bool i_j = false;
            //filtering key from repeated letters
            foreach (char letter in key) {

                if (!filtered.Contains(letter))
                {
                #region i_or_j
                    if ((letter == 'i' || letter == 'j'))
                    {
                        if (!i_j)
                        {
                            i_j = true;
                            filtered += letter; 
                        }
                        continue;
                    }
                 #endregion
                    filtered += letter;
                }
            }
           
          
            //Console.WriteLine(i + " " + j);
            int num_letter = 97;
            while (filtered.Length<25) {
                char padded_letter = (char)num_letter;
                if (!filtered.Contains(padded_letter))
                {

                    #region i_or_j
                    if ((padded_letter == 'i' || padded_letter == 'j'))
                    {
                        if (!i_j)
                        {
                            i_j = true;
                            filtered += padded_letter;
                        }
                    }
                    else
                    {
                        #endregion
                        filtered += padded_letter;
                    }
                }
                num_letter++;
            }
            //Console.WriteLine(i + " " + j);
            //Console.WriteLine(filtered[filtered.Length-1]);
       


            char[,] matrix = new char[5, 5];
            int i = 0, j = 0;
            foreach (char x in filtered)
            {
                matrix[i, j] = x;
                if (j == 4)
                {
                    j = 0;
                    i++;
                }
                else { j++; }
            }
            for (int u = 0; u < 5; u++) {
                for (int f = 0; f < 5; f++) {
                    Console.WriteLine(matrix[u, f]);
                }
            }





            throw new NotImplementedException();
        }
    }
}
