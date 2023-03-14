using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>

    {
        public static List<List<int>> GeneratePermutations(int n)
        {
            List<int> nums = Enumerable.Range(1, n).ToList();
            List<List<int>> permutations = new List<List<int>>();
            Permute(nums, 0, n - 1, permutations);
            return permutations;
        }

        public static void Permute(List<int> nums, int left, int right, List<List<int>> permutations)
        {
            if (left == right)
            {
                permutations.Add(new List<int>(nums));
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    Swap(nums, left, i);
                    Permute(nums, left + 1, right, permutations);
                    Swap(nums, left, i);
                }
            }
        }

        public static void Swap(List<int> nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public List<int> Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();




            List<List<int>> allPermutations = new List<List<int>>();

            List<int> keys = new List<int>();



            for (int i = 1; i < 8; i++)
            {


                allPermutations = GeneratePermutations(i);

                for (int j = 0; j < allPermutations.Count(); j++)
                {
                    string mycip = Encrypt(plainText, allPermutations[j]);


                    if (cipherText.Equals(mycip, StringComparison.InvariantCultureIgnoreCase))
                    {


                        return allPermutations[j];

                    }
                }

            }



            List<int> zerosList = new List<int>();


            for (int i = 0; i < plainText.Length; i++)
            {
                zerosList.Add(0);
            }


            return zerosList;

        }

        public string Decrypt(string cipherText, List<int> key)
        {
            // throw new NotImplementedException();


            string plainText = "";
            int col = key.Count();
            int len = cipherText.Length;

            int row = (int)Math.Ceiling((decimal)(len / col));

            char[,] cipher_arr = new char[row, col];

            int index = 0;
            for (int i = 0; i < col; i++)
            {

                for (int j = 0; j < row; j++)
                {
                    int k = key.IndexOf(i + 1);

                    try
                    {
                        if (index < cipherText.Length)
                            cipher_arr[j, k] = cipherText[index];
                        index++;
                    }
                    catch (Exception e)
                    {


                    }



                }
            }

            for (int i = 0; i < row; i++)
            {

                for (int j = 0; j < col; j++)
                {
                    try { plainText += cipher_arr[i, j]; }
                    catch (Exception e) { }



                }
            }

            return plainText;



        }
        public string Encrypt(string plainText, List<int> key)
        {

            int depth = (int)Math.Ceiling((decimal)plainText.Length / key.Count);
            //3mlt matrix a7ot fiha al plaintext
            char[,] matrix = new char[depth, key.Count];
            for (int row = 0; row < depth; row++) //5 col
            {
                for (int col = 0; col < key.Count; col++)
                {
                    try
                    {
                        matrix[row, col] = plainText[row * key.Count + col];
                    }
                    catch (Exception e) //lw alklma 5lset w lessa fe mkan fadi a7ot 'x'
                    {

                        matrix[row, col] = 'x';

                    }
                }
            }
            //3mlt 3 for loop goa b3d , wa7da bm4i 3la al columns (no of keys)
            //altania bm4i 3la al columns brdo leh ? 34an al key 3obara 3n 1 ,3 ,5,4,2 msln 
            //y3ni ana a7ot awel column w 5 colmn b3dha tani column b3do 4 column s7?
            //f h3ml index =0 w hm4i akaren ben al index w alrkn ali goa awl mkan fel key ali hoa 1
            //if (key[k]-1 ==i) y3ni (0==0)? yes 5las += al column bta3 index w hkza
            string ecrip = "";
            for (int i = 0; i < key.Count; i++)
            {
                int index = 0;
                for (int k = 0; k < key.Count; k++)
                    //Console.WriteLine(ecrip);
                    if (key[k] - 1 == i)
                    {
                        //de btm4i 3la al column w ta5od al 7rof ali fih
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

