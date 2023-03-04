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
          

            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
         //   Console.WriteLine("text: "+plainText + "\n" +"key: "+ key);
            string filtered = "";
            bool i_j = false;
            #region filtering key
            //filtering key from repeated letters
            foreach (char letter in key) {

                if (!filtered.Contains(letter))
                {
                    if ((letter == 'i' || letter == 'j'))
                    {
                        if (!i_j)
                        {
                            i_j = true;
                            filtered += letter; 
                        }
                        continue;
                    }
                    filtered += letter;
                }
            }

            //output is key after filtered
            #endregion

            #region padding_key
            //padding key
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
                //Console.WriteLine(filtered);

            }
            #endregion

            for (int q = 0; q < 25; q+=5) {
                Console.WriteLine(filtered[q]+" "+filtered[q+1]+" "+ filtered[q+2] + " "+ filtered[q+3] + " "+filtered[q+4]);
            }
            char[,] key_matrix = new char[5, 5];
            int i = 0, j = 0;
            foreach (char x in filtered)
            {
                key_matrix[i, j] = x;
                if (j == 4)
                {
                    j = 0;
                    i++;
                }
                else { j++; }
            }





            #region dividing word
            List<List<char>> words = new List<List<char>>();
            List<char> word = new List<char>();
            int iterator = 0;
            for (int k=0; k < plainText.Count(); k++) {
                if (k == plainText.Count() - 1 && iterator == 0)
                {
                    word.Add(plainText[k]);
                    word.Add('x');
                    words.Add(word);
                  //Console.WriteLine(words[words.Count - 1][0] + " " + words[words.Count - 1][1]);

                    break;
                }
                if (iterator == 1 && word[0] == plainText[k])
                {
                    k -=1;
                    word.Add('x');

                }
                else
                {
                    word.Add(plainText[k]);
                }
               
                iterator++;
                
                if (iterator == 2) { 
                    iterator = 0;
                    words.Add(word);
                 //  Console.WriteLine(words[words.Count-1][0]+" "+words[words.Count - 1][1]);
                    word = new List<char>();
                }
            }
            #endregion

            string encripted = "";
            foreach (List<char> pair in words) {
            //    Console.WriteLine("in: "+pair[0]+" "+pair[1]);
                int index_i1=0, index_j1=0, index_i2=0, index_j2=0;
                bool bool1=false, bool2=false;
                #region get index of pairs
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        if (pair[0] == key_matrix[i, j])
                        {
                            index_i1 = i;
                            index_j1 = j;
                            bool1 = true;
                        }
                        if (pair[1] == key_matrix[i, j])
                        {
                            index_i2 = i;
                            index_j2 = j;
                            bool2 = true;
                        }
                        if (bool1 && bool2)
                            break;
                    }
                    if (bool1 && bool2)
                    {
                     //   Console.WriteLine("out: "+pair[0] + "  "+ pair[1]);
                        break;
                    }
                }
                #endregion
                #region encription
                if (index_j1 == index_j2) {
                    try
                    {
                        encripted += key_matrix[index_i1 + 1, index_j1];
                    }
                    catch (Exception e) {
                        encripted += key_matrix[0, index_j1];
                    }
                    try
                    {
                        encripted += key_matrix[index_i2 + 1, index_j2];
                    }
                    catch (Exception e) {
                        encripted += key_matrix[0, index_j2];
                    }
                }
                else if(index_i1 == index_i2){
                    try
                    {
                        encripted += key_matrix[index_i1, index_j1 + 1];
                    }
                    catch (Exception e) {
                        encripted += key_matrix[index_i1, 0];
                    }
                    try
                    {
                        encripted += key_matrix[index_i2, index_j2 + 1];
                    }
                    catch (Exception e) {
                        encripted += key_matrix[index_i2, 0];
                    }
                //    Console.WriteLine(encripted);
                }
                else {
                encripted+= key_matrix[index_i1, index_j2];
                encripted += key_matrix[index_i2, index_j1];
                }
             //   Console.WriteLine(encripted);
            }
            #endregion

            return encripted;
        }
    }
}
