using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
           
            int m = (int)Math.Sqrt(key.Count);
            int[,] key_matrix = new int[m, m];
            //key_matrix
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < m; col++)
                {

                    key_matrix[row, col] = key[row * m + col];
                }
            }
            int n = plainText.Count / m;
            Console.WriteLine(n);
            int[,] plain_matrix = new int[m, n];
            //key_plain
            for (int col = 0; col < n; col++)
            {
                for (int row = 0; row < m; row++)
                {
                    plain_matrix[row, col] = plainText[col * m + row];
                    
                }
            }

            //key(2,2)
            //plain(2,6)
            List<int> encr = new List<int>();
            for (int col = 0; col < n; col++) //bm4i 3la kol alcolumns al 6
            {
                for (int row = 0; row < m; row++) //3la row
                {
                    int sum_row_column = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum_row_column += key_matrix[row, k] * plain_matrix[k, col];
                    }
                    encr.Add(sum_row_column % 26);
                }

            }
            return encr;
        }      


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}

