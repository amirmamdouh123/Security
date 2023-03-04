using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        string alphabets = "abcdefghiklmnopqrstuvwxyz";



        char[,] cipher_matrix = new char[5, 5];
        public char[,] updatedtedKey(string key)
        {
            HashSet<char> Gkey = new HashSet<char>();
            int keyLength = key.Length;
            HashSet<char>.Enumerator em = Gkey.GetEnumerator();
            for (int i = 0; i < keyLength; i++)
            {
                if (key[i] == 'j')
                {
                    Gkey.Add('i');
                }
                else
                {
                    Gkey.Add(key[i]);
                }
            }

            for (int i = 0; i < 25; i++)
            {
                //without j
                Gkey.Add(alphabets[i]);
            }
            for (int i = 0; i < 25; i++)
            {
                Gkey.Add(alphabets[i]);
            }
            int row = 0, col = 0;
            foreach (var v in Gkey)
            {

                cipher_matrix[col, row] = v;
                //Console.WriteLine(cipher_matrix[row, col]);
                row = (row + 1) % 5;
                if (row == 0)
                {
                    col++;
                }

            }
            return cipher_matrix;
        }



        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            string plain_text = "";
            char[,] cipher_matrix = updatedtedKey(key.ToLower());
            List<string> blocks = new List<string>();
            int CLT = cipherText.Length - 1;
            for (int i = 0; i < CLT; i += 2)
            {
                blocks.Add(cipherText.Substring(i, 2));
            }

            Console.WriteLine(blocks.Count);

            for (int z = 0; z < blocks.Count; z++)
            {      // 2 bool for each point
                bool found_point1 = false, found_point2 = false;
                int pointrow1 = 0, pointrow2 = 0, pointcol1 = 0, pointcol2 = 0;
                // intilize matrix[5*5]
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {     //first point
                        if (blocks[z][0] == cipher_matrix[i, j])
                        {
                            pointcol1 = i;
                            pointrow1 = j;

                            found_point1 = true;
                        }
                        // sec point
                        if (blocks[z][1] == cipher_matrix[i, j])
                        {
                            pointcol2 = i;
                            pointrow2 = j;

                            found_point2 = true;
                        }
                        //كدا حددت مكان النقطتين هعمل ال تلت حالات
                        if (found_point1 && found_point2)
                        {
                            //if same row
                            if (pointrow1 == pointrow2)
                            {
                                plain_text += cipher_matrix[(pointcol1 + 4) % 5, pointrow1];
                                plain_text += cipher_matrix[(pointcol2 + 4) % 5, pointrow2];
                            }
                            //if same col 
                            else if (pointcol1 == pointcol2)
                            {
                                plain_text += cipher_matrix[pointcol1, (pointrow1 + 4) % 5];
                                plain_text += cipher_matrix[pointcol2, (pointrow2 + 4) % 5];
                            }
                            // niether
                            else
                            {
                                plain_text += cipher_matrix[pointcol1, pointrow2];
                                plain_text += cipher_matrix[pointcol2, pointrow1];
                            }
                            break;
                        }
                    }
                    if (found_point1 && found_point2) break;
                }
            }
            blocks = new List<string>();
            // احنا شغالين حرفين حرفين
            for (int i = 0; i < plain_text.Length - 1; i += 2)
            {
                blocks.Add(plain_text.Substring(i, 2));
            }
            //x عندى حرف متكرر وهحط 
            int changable_index = 0;
            for (int b = 0; b < blocks.Count - 1; b++)
            {
                Console.WriteLine(blocks[b]);
                if (blocks[b][1] == 'x' && blocks[b][0] == blocks[b + 1][0])
                {
                    plain_text = plain_text.Remove(b * 2 + 1 + changable_index, 1);
                    changable_index--;

                }
            }
            //x لو كانت فردى و اخر حرف 
            if (plain_text[plain_text.Length - 1] == 'x')
            {
                plain_text = plain_text.Remove(plain_text.Length - 1);
            }
            Console.WriteLine(plain_text);
            Console.WriteLine(plain_text.Length);
            return plain_text.ToUpper();
        }

        public string Encrypt(string plainText, string key)
        {
            //   Console.WriteLine("text: "+plainText + "\n" +"key: "+ key);
            char[,] key_matrix = updatedtedKey(key);

            #region dividing word into pairs
            List<List<char>> words = new List<List<char>>();
            List<char> word = new List<char>();
            int iterator = 0;
            for (int k = 0; k < plainText.Count(); k++) {
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
                    k -= 1;
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
                int index_i1 = 0, index_j1 = 0, index_i2 = 0, index_j2 = 0;
                bool bool1 = false, bool2 = false;
                #region get index of pairs
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
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
                else if (index_i1 == index_i2) {
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
                    encripted += key_matrix[index_i1, index_j2];
                    encripted += key_matrix[index_i2, index_j1];
                }
                //   Console.WriteLine(encripted);
            }
            #endregion

            return encripted;
        }
    }
}

