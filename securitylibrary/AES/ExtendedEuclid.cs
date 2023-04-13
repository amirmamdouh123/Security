using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            //throw new NotImplementedException();
            List<int> A = new List<int>
            {
              -1 , 1,0,baseN
            };
            List<int> B = new List<int>

            {
               -1, 0,1,number
            };
            List<int> M = new List<int>() {
                -1, 0, 0, 0,
            };

            int q;
            while (true)
            {
                if (B[3] == 0)
                { return -1; }
                else if (B[3] == 1)
                {
                    return ((B[2] % baseN) + baseN) % baseN;
                }
                q = A[3] / B[3];

                M[1] = A[1] - q * B[1];
                M[2] = A[2] - q * B[2];
                M[3] = A[3] - q * B[3];

                A[1] = B[1];
                A[2] = B[2];
                A[3] = B[3];


                B[1] = M[1];
                B[2] = M[2];
                B[3] = M[3];

            }

        }
    }
}