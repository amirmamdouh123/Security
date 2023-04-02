using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    /// 
    public class AES : CryptographicTechnique
    {


        // 2semna al key w al plain l matrces w b3d kda 7welnahom binary w b3d kda xor w b3d kda drbt al xor fe s_box(8*8)+(8,) bs fe 5atwa na2sa abl ma adrb al xor 


        public int[,] s_box()
        {
            int[,] matrix = new int[8, 8];
            int[] first_row = { 1, 0, 0, 0, 1, 1, 1, 1 };
            for (int i = 0; i < 8; i++)
            {
                matrix[0, i] = first_row[i];
            }

            for (int i = 1; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        matrix[i, j] = matrix[i - 1, 7];
                        continue;
                    }
                    matrix[i, j] = matrix[i - 1, j - 1];
                }
            }


            return matrix;
        }


        public string[,] xor(string[,] plain_matrix, string[,] key_matrix, int m)
        {


            string plain_binary;
            string key_binary;

            string[,] result = new string[m, m];
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {

                    #region binary

                    string xor = ""; //[b0,b1,b2,b3,b4,b5,b6,b7]
                    plain_binary = String.Join(String.Empty, plain_matrix[j, i].ToUpper().Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                    key_binary = String.Join(String.Empty, key_matrix[j, i].ToUpper().Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));


                    #endregion binary

                    #region xor

                    for (int k = 0; k < 8; k++)
                    {
                        if (plain_binary[k] == key_binary[k])
                        {
                            xor += '0';
                        }
                        else xor += '1';
                    }
                    // string v = String.Format("{0:X2}", Convert.ToUInt64(xor, 2));
                    #endregion xor

                    xor = String.Join(String.Empty, "19".Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

                    Console.WriteLine(xor);
                    #region s_box
                    int[,] s = s_box();
                    int[] plus = { 1, 1, 0, 0, 0, 1, 1, 0 };

                    string[] result_mult = new string[8];
                    string[] after_sbox = new string[2];

                    #region muliplication_s_box
                    for (int row = 0; row < 8; row++)
                    {
                        Console.WriteLine(s[row, 0] + " " + s[row, 1] + " " + s[row, 2] + " " + s[row, 3] + " " + s[row, 4] + " " + s[row, 5] + " " + s[row, 6] + " " + s[row, 7]);
                        Console.WriteLine(xor[7 - 0] + " " + xor[7 - 1] + " " + xor[7 - 2] + " " + xor[7 - 3] + " " + xor[7 - 4] + " " + xor[7 - 5] + " " + xor[7 - 6] + " " + xor[7 - 7] + "\n");

                        Console.WriteLine(plus[row] + "\n");

                        for (int col = 0; col < 8; col++)
                        {
                            if (s[row, col] == 1 && xor[7 - col] == '1')
                            {
                                result_mult[row] += '1';
                            }

                        }
                        if (plus[row] == 1)
                        {
                            result_mult[row] += '1';
                        }
                        Console.WriteLine("result: " + result_mult[row]);
                        //Console.WriteLine("row: "+row);
                        //Console.WriteLine(result_mult[row]);

                    }
                    #endregion muliplication_s_box


                    for (int a = 0; a < 8; a++)
                    {
                        try
                        {
                            if (result_mult[a].Length % 2 == 0)
                            {

                                after_sbox[(a / 4)] += '0';
                            }
                            else
                            {
                                after_sbox[(a / 4)] += '1';
                            }
                        }
                        catch (Exception e)
                        {

                            after_sbox[1 - (a / 4)] += '0';
                        }

                    }


                    char[] reverse1 = after_sbox[0].ToCharArray();
                    Array.Reverse(reverse1);
                    after_sbox[0] = new string(reverse1);
                    char[] reverse2 = after_sbox[1].ToCharArray();
                    Array.Reverse(reverse2);
                    after_sbox[1] = new string(reverse2);

                    //   Console.WriteLine(after_sbox[0]+ "  "+ after_sbox[1]);

                    #endregion s_box
                    string letter1 = String.Format("{0:X2}", Convert.ToUInt64((after_sbox[1] + after_sbox[0]), 2));

                    result[j, i] = letter1;

                }
            }
            return result;
        }





        public string[,] to_matrix(string arr)
        {
            string a = arr.ToString();
            int m = (int)Math.Sqrt((double)a.Length / 2);
            string[,] matrix = new string[m, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m * 2; j++)
                {
                    matrix[j / 2, i] += a[i * (m * 2) + j];
                }
            }


            return matrix;
        }


        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            plainText = "3243f6a8885a308d313198a2e0370734";
            key = "2b7e151628aed2a6abf7158809cf4f3c";
            int m = (int)Math.Sqrt((double)key.Length / 2);

            string[,] f = xor(to_matrix(plainText), to_matrix(key), m);


            for (int i = 0; i < m; i++)
            {
                Console.WriteLine(f[i, 0] + " " + f[i, 1] + " " + f[i, 2] + " " + f[i, 3]);
            }



            throw new NotImplementedException();
        }

    }
}
