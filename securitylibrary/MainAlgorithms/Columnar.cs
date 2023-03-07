using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, List<int> key)
        {

            int depth = (int) Math.Ceiling((decimal)plainText.Length / key.Count);
            char[,] matrix = new char[depth, key.Count];
            for (int row = 0; row < depth; row++) //5 col
            {
                for (int col = 0; col < key.Count; col++)
                {
                    try
                    {
                        matrix[row, col] = plainText[row * key.Count + col];
                    }
                    catch(Exception e) 
                    {

                        matrix[row, col] = 'x';

                    }
                }
            }

            string ecrip = "";
            for (int i = 0; i < key.Count; i++)
            {
                int index = 0;
                for (int k = 0; k < key.Count; k++)
                    if (key[k] - 1 == i)
                    {
                        for (int j = 0; j < depth; j++)
                        {

                            ecrip += matrix[j, index];
                        }
                    }
                    else { index++; }
            }
            
            return ecrip;
        }
    }
}
