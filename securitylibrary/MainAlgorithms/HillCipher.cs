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



        public int[,] to_matrix(List<int> key)
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
            return key_matrix;
        }


        public int determ3(int[,] pMatrix, int size)
        {
            int det = 0;
            if (size == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    det = det + (pMatrix[0, i] * (pMatrix[1, (i + 1) % 3] * pMatrix[2, (i + 2) % 3] - pMatrix[1, (i + 2) % 3] * pMatrix[2, (i + 1) % 3]));

                }
                det = det % 26;
                if (det == 15)
                {
                    det = 7;
                }
                else if (det == 7)
                {
                    det = 15;
                }
            }
            else if (size == 2)
            {
                det = pMatrix[0, 0] * pMatrix[1, 1] - pMatrix[1, 0] * pMatrix[0, 1];
            }
            return det;
        }










        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {


            List<int> key = new List<int>();
            bool flag = false;
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        for (int m = 0; m < 26; m++)
                        {
                            key = Encrypt(plainText, new List<int> { m, k, j, i });

                            flag = Enumerable.SequenceEqual(key, cipherText);
                            if (flag)
                            {
                                return new List<int> { m, k, j, i };
                            }
                            else
                                continue;
                        }
                    }
                }
            }
            if (!flag)
                throw new InvalidAnlysisException();
            else
                return key;
        }




        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            int m = (int)Math.Sqrt(key.Count);

            //for (int i = 0; i < 4; i++) { 
            //Console.WriteLine(key[i]);
            //}
            int[,] key_matrix = to_matrix(key);

            decimal det = determ3(key_matrix,m);

           // Console.WriteLine(determ3(key_matrix, m));


            decimal temp = 0;

            while (((26 - temp) * (det))%26!=1) {
                temp++;
            }
            decimal det_inverse = (26 - (temp% 26));
           Console.WriteLine(det_inverse);
            

            decimal[,] inverse=new decimal[2,2] ;

            if (m == 2) {

                inverse = new decimal[2, 2];


                for (int row = 0; row < m; row++)
                {
                    for (int col = 0; col < m; col++)
                    {
                        if (row == col)
                        {
                            inverse[row, col] =(det_inverse* key_matrix[(row + 1) % 2, (col + 1) % 2])%26;
                            if (inverse[row, col] < 0)
                            {
                                inverse[row, col] += 26;
                            }
                        }
                        else {
                            inverse[row, col] =(det_inverse* -key_matrix[row,col])%26;
                            if (inverse[row, col] < 0)
                            {
                                inverse[row, col] += 26;
                            }

                        }
                    }
                }

            }
            else if (m == 3)
            {
                inverse = new decimal[3, 3];
                for (int row = 0; row < m; row++)
                {
                    for (int col = 0; col < m; col++)
                    {

                        inverse[row, col] = (det_inverse * (key_matrix[(row + 1) % 3, (col + 1) % 3] * key_matrix[(row + 2) % 3, (col + 2) % 3] - key_matrix[(row + 2) % 3, (col + 1) % 3] * key_matrix[(row + 1) % 3, (col + 2) % 3]) % 26);
                        if (inverse[row, col] < 0)
                            inverse[row, col] += 26;
                    }
                }
            }
         
            List<int> transpose =new List<int>();
            if (m == 2)
            {
                for (int row = 0; row < m; row++)
                {
                    for (int col = 0; col < m; col++)
                    {

                        transpose.Add((int)inverse[row, col]);
                    }
                }
            }
            else if (m == 3)
            {
                for (int col = 0; col < m; col++)
                {

                    Console.WriteLine(inverse[col, 0] + " " + inverse[col, 1] + " " + inverse[col, 2]);
                }

                for (int row = 0; row < m; row++)
                {
                    for (int col = 0; col < m; col++)
                    {

                        transpose.Add((int)inverse[col, row]);
                    }
                }
            }
            return Encrypt(cipherText, transpose);
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int m = (int)Math.Sqrt(key.Count);
            int[,] key_matrix = to_matrix(key);
            int n = plainText.Count / m;
           // Console.WriteLine(m);
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
                   // Console.WriteLine(sum_row_column %26);
                }

            }
            return encr;
        }


        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {

            int[,] cMatrix = new int[3, 3];
            int[,] New_pMatrix = new int[3, 3];
            int[,] pMatrix = new int[3, 3];
            int[,] keymatrix = new int[3, 3];
            int count = 0;
            int det = 0;



            for (int i = 0; i < 3; i++)
                det = det + (pMatrix[0, i] * (pMatrix[1, (i + 1) % 3] * pMatrix[2, (i + 2) % 3] - pMatrix[1, (i + 2) % 3] * pMatrix[2, (i + 1) % 3]));

            det = ((det % 26) + 26) % 26;



            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cMatrix[i, j] = cipher3[count++] % 26;
                }
            }

            count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pMatrix[j, i] = plain3[count++] % 26;
                }
            }

            det += determ3(pMatrix, 3);


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((i == 0) && (j == 0))
                    {

                        New_pMatrix[i, j] = (det * (pMatrix[1, 1] * pMatrix[2, 2] - pMatrix[1, 2] * pMatrix[2, 1])) % 26;
                    }
                    else if ((i == 0) && (j == 1))
                    {
                        New_pMatrix[i, j] = (det * ((-1) * (pMatrix[1, 0] * pMatrix[2, 2] - pMatrix[1, 2] * pMatrix[2, 0]))) % 26;
                    }
                    else if ((i == 0) && (j == 2))
                    {
                        New_pMatrix[i, j] = (det * (pMatrix[1, 0] * pMatrix[2, 1] - pMatrix[1, 1] * pMatrix[2, 0])) % 26;
                    }
                    else if ((i == 1) && (j == 0))
                    {
                        New_pMatrix[i, j] = (det * ((-1) * (pMatrix[0, 1] * pMatrix[2, 2] - pMatrix[2, 1] * pMatrix[0, 2]))) % 26;
                    }
                    else if ((i == 1) && (j == 1))
                    {
                        New_pMatrix[i, j] = (det * (pMatrix[0, 0] * pMatrix[2, 2] - pMatrix[2, 0] * pMatrix[0, 2])) % 26;
                    }
                    else if ((i == 1) && (j == 2))
                    {
                        New_pMatrix[i, j] = (det * ((-1) * (pMatrix[0, 0] * pMatrix[2, 1] - pMatrix[2, 0] * pMatrix[0, 1]))) % 26;
                    }
                    else if ((i == 2) && (j == 0))
                    {
                        New_pMatrix[i, j] = (det * (pMatrix[0, 1] * pMatrix[1, 2] - pMatrix[0, 2] * pMatrix[1, 1])) % 26;
                    }
                    else if ((i == 2) && (j == 1))
                    {
                        New_pMatrix[i, j] = (det * ((-1) * (pMatrix[0, 0] * pMatrix[1, 2] - pMatrix[1, 0] * pMatrix[0, 2]))) % 26;
                    }
                    else if ((i == 2) && (j == 2))
                    {
                        New_pMatrix[i, j] = (det * (pMatrix[0, 0] * pMatrix[1, 1] - pMatrix[1, 0] * pMatrix[0, 1])) % 26;
                    }
                }

            }




            for (int a = 0; a < 3; a++) // becuase all is mod 26
            {
                for (int j = 0; j < 3; j++)
                {
                    if (New_pMatrix[a, j] < 0)
                    {
                        New_pMatrix[a, j] += 26;
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((i == 0) && (j == 0))
                    {

                        keymatrix[i, j] = (New_pMatrix[0, 0] * cMatrix[0, 0] + New_pMatrix[0, 1] * cMatrix[1, 0] + New_pMatrix[0, 2] * cMatrix[2, 0]) % 26;
                    }

                    else if ((i == 1) && (j == 0))
                    {
                        keymatrix[i, j] = (New_pMatrix[0, 0] * cMatrix[0, 1] + New_pMatrix[0, 1] * cMatrix[1, 1] + New_pMatrix[0, 2] * cMatrix[2, 1]) % 26;
                    }
                    else if ((i == 2) && (j == 0))
                    {
                        keymatrix[i, j] = (New_pMatrix[0, 0] * cMatrix[0, 2] + New_pMatrix[0, 1] * cMatrix[1, 2] + New_pMatrix[0, 2] * cMatrix[2, 2]) % 26;
                    }
                    else if ((i == 0) && (j == 1))
                    {
                        keymatrix[i, j] = (New_pMatrix[1, 0] * cMatrix[0, 0] + New_pMatrix[1, 1] * cMatrix[1, 0] + New_pMatrix[1, 2] * cMatrix[2, 0]) % 26;
                    }
                    else if ((i == 1) && (j == 1))
                    {
                        keymatrix[i, j] = (New_pMatrix[1, 0] * cMatrix[0, 1] + New_pMatrix[1, 1] * cMatrix[1, 1] + New_pMatrix[1, 2] * cMatrix[2, 1]) % 26;
                    }
                    else if ((i == 2) && (j == 1))
                    {
                        keymatrix[i, j] = (New_pMatrix[1, 0] * cMatrix[0, 2] + New_pMatrix[1, 1] * cMatrix[1, 2] + New_pMatrix[1, 2] * cMatrix[2, 2]) % 26;
                    }
                    else if ((i == 0) && (j == 2))
                    {
                        keymatrix[i, j] = (New_pMatrix[2, 0] * cMatrix[0, 0] + New_pMatrix[2, 1] * cMatrix[1, 0] + New_pMatrix[2, 2] * cMatrix[2, 0]) % 26;
                    }
                    else if ((i == 1) && (j == 2))
                    {
                        keymatrix[i, j] = (New_pMatrix[2, 0] * cMatrix[0, 1] + New_pMatrix[2, 1] * cMatrix[1, 1] + New_pMatrix[2, 2] * cMatrix[2, 1]) % 26;
                    }
                    else if ((i == 2) && (j == 2))
                    {
                        keymatrix[i, j] = (New_pMatrix[2, 0] * cMatrix[0, 2] + New_pMatrix[2, 1] * cMatrix[1, 2] + New_pMatrix[2, 2] * cMatrix[2, 2]) % 26;
                    }
                }

            }



            List<int> key = new List<int>();
            for (int a = 0; a < 3; a++)
            {
                for (int j = 0; j < 3; j++)
                {
                    key.Add(keymatrix[a, j]);
                }
            }
            return key;
        }
    }
}




