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
        #region mix_matrix
        byte[,] mix_mat = new byte[4, 4] { { 0x02, 0x03, 0x01, 0x01 },
                                          { 0x01, 0x02, 0x03, 0x01 },
                                          { 0x01, 0x01, 0x02, 0x03 },
                                          { 0x03, 0x01, 0x01, 0x02 } };

        #endregion mix_matrix

        #region s_box
        byte[,] sBox = new byte[16, 16] {
    { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 },
    { 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0 },
    { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15 },
    { 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75 },
    { 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84 },
    { 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf },
    { 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8 },
    { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2 },
    { 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73 },
    { 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb },
    { 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79 },
    { 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08 },
    { 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6,0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a },
    { 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e,0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e },
    { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf },
    { 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68,0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 } };
        #endregion

        public string[,] to_matrix(string arr)
        {
            string a = arr.ToString();
            //-2 to skip the first to char '0x'
            int m = (int)Math.Sqrt((double)(a.Length-2) / 2);
            string[,] matrix = new string[m, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m * 2; j++)
                {        //+2 to skip the first to char '0x'
                    matrix[j / 2, i] += a[i * (m * 2) + j+2];
                }
            }


            return matrix;
        }

        public string XOR(string a,string b,int size) {
            string xor="";
            for (int k = 0; k < size; k++)
            {
                if (a[k] == b[k])
                {
                    xor += '0';
                }
                else xor += '1';
            }
            return xor;
        }



        public string[,] s_box(string[,] plain_matrix, string[,] key_matrix, int m)
        {
            string[,] sBoxString = new string[16, 16];
            #region convert s_box_toString    
            for (int z = 0; z < 16; z++)
            {
                for (int s = 0; s < 16; s++)
                {
                    sBoxString[s, z] = sBox[s, z].ToString("X2");
                }
            }
            #endregion


            string plain_binary;
            string key_binary;

            string[,] result = new string[m, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {

                    #region binary

                    string xor = ""; //[b0,b1,b2,b3,b4,b5,b6,b7]
                    plain_binary = String.Join(String.Empty, plain_matrix[j, i].ToUpper().Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                    key_binary = String.Join(String.Empty, key_matrix[j, i].ToUpper().Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));


                    #endregion binary

                    #region xor

                    xor = XOR(plain_binary,key_binary, plain_binary.Length);
                    // string v = String.Format("{0:X2}", Convert.ToUInt64(xor, 2));
                    #endregion xor

                    string letter = String.Format("{0:X2}", Convert.ToUInt64((xor), 2));//hexa

                   // Console.WriteLine(letter);
                    int decValue1 = int.Parse(letter[0].ToString(), System.Globalization.NumberStyles.HexNumber);
                    int decValue2 = int.Parse(letter[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    
               

                    result[j, i] = sBoxString[decValue1, decValue2];
                }
            }
            return result;
        }


        public string[,] shift(string[,] matrix, int m) {

            string[,] after_shift = new string[m,m];
            for(int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++) {
                    try
                    {
                        after_shift[i, j-i] = matrix[i, j];
                    }
                    catch (Exception e) {
                        after_shift[i, j-i+4] = matrix[i, j];
                    }
                }
            }
            return after_shift;
        }


        public byte[,] to_byte_matrix(string[,] matrix){

            byte[,] byte_matrix = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    byte_matrix[i, j] = byte.Parse(matrix[i, j], System.Globalization.NumberStyles.HexNumber);
                    
                }
            }
            return byte_matrix;
        }



        public byte[,] mix_column(byte[,] matrix)
        {
            //mix matrix is defined globally
            byte[,] result = new byte[4, 4];

            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    byte sum = 0;

                    for (int k = 0; k < 4; k++)
                    {

                        byte matrix_byte = matrix[k, i];
                        byte mix_byte = mix_mat[j, k];

                        if (mix_byte == 1)
                        {   //xor
                            sum ^= matrix_byte;
                        }

                        else if (mix_byte == 2)
                        {
                            //mix_matply if a =2
                            byte left_shift = (byte)(matrix_byte << 1);
                            byte highBit = (byte)(matrix_byte >> 7);

                            byte xor = (byte)(left_shift ^ (0x1B & -highBit));
                            //xor
                            sum ^= xor;
                        }

                        else if (mix_byte == 3)
                        {   //mix_matply if a =3 
                            byte left_shift = (byte)(matrix_byte << 1);
                            byte highBit = (byte)(matrix_byte >> 7);

                            byte xor = (byte)(left_shift ^ matrix_byte ^ (0x1B & -highBit));
                            //xor
                            sum ^= xor;
                        }

                    }
                    
                    result[j, i] = sum;
                }
            }
            return result;
        }
       //10 is no of rounds
        byte[] rcon = new byte[10] {0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80,0x1B,0x36}; 

        public byte[,] generate_key_round(byte[,] key,int round) {
            
            byte[,] new_key = new byte[4,4];



            for (int d = 0; d <4; d++)
            {
                int x = d-1;
                //first col
                if (d == 0)
                {
                    x = 3;
                    //shift w[3] d0,d1,d2,d3   -> (d1,d2,d3,d0) ---> s_box (s1,s2,s3,s0)
                    for (int i = 0; i < 4; i++){
                        byte r;
                        byte for_xor = new byte();
                        if (i == 3)
                            {
                                //shift
                                string l = key[0, 3].ToString("X2");
                                int dec1 = int.Parse(l[0].ToString(), System.Globalization.NumberStyles.HexNumber);
                                int dec2 = int.Parse(l[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                                //get corresponding in sBox
                                for_xor = sBox[dec1, dec2];
                          // Console.WriteLine(l+" "+for_xor.ToString("X2"));

                        }
                        else
                            {   //shift
                                string l = key[i+1, 3].ToString("X2");
                                int dec1 = int.Parse(l[0].ToString(), System.Globalization.NumberStyles.HexNumber);
                                int dec2 = int.Parse(l[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                                //get corresponding in sBox
                                for_xor = sBox[dec1, dec2];
                          // Console.WriteLine(l + " " + for_xor.ToString("X2"));

                        }

                        if (i == 0)
                        {
                            r = rcon[round - 1];
                        }
                        else {
                            r = 0;
                        }
                        //Console.WriteLine(key[i, d].ToString("X2")+" "+ for_xor.ToString("X2")+" "+ r.ToString("X2")) ;
                        //Console.WriteLine((for_xor ^ rcon[round - 1]).ToString("X2"));
                        new_key[i, 0] = (byte)(key[i, d] ^ for_xor ^ r);

                    }
                }
                else {
                    for (int j = 0; j < 4; j++)
                    {
                        // Console.WriteLine(key[d, j].ToString("X2") + " " + for_xor.ToString("X2") + " " + r.ToString("X2"));
                        if (j == 0)
                        {
                            new_key[j, d] = (byte)(key[j, d] ^ new_key[j, x]);

                        }
                        else
                        {
                            new_key[j, d] = (byte)(key[j, d] ^ new_key[j, x]);
                        }
                    }

                }

                }
            return new_key;
        }


        public byte[,] xor_byte(byte[,] matrix1, byte[,] matrix2)
        {

            byte[,] result = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    result[i, j] = (byte)(matrix1[i, j] ^ matrix2[i, j]);
                }
            }


            return result;
        }






        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {

            int m = (int)Math.Sqrt((double)key.Length / 2);

            //s_box step
            #region test_key
            //string[,] test_round_key = new string[4, 4] { {"0f","15","71","c9" },
            //                                            { "47","d9","e8","59"},
            //                                            {"0c","b7","ad","ad" },
            //                                            { "af","7f","67","98"} };
            #endregion



            string[,] f = s_box(to_matrix(plainText), to_matrix(key), m); //subbyte step

            string[,] shifted_matrix= shift(f, m); //shift row step
            byte[,] bytee=to_byte_matrix(shifted_matrix); //convert string matrix to byte matrix
           byte[,]after_mix= mix_column(bytee);            //mix_column step
            byte[,] round1 =generate_key_round(to_byte_matrix(to_matrix(key)), 1); //generate key round
            byte[,] addroundKey = xor_byte(round1,after_mix); //addroundKey step
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(addroundKey[i, 0].ToString("X2") + " " + addroundKey[i, 1].ToString("X2") + " " + addroundKey[i, 2].ToString("X2") + " " + addroundKey[i, 3].ToString("X2"));
            }

            throw new NotImplementedException();
        }

    }
}
