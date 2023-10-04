using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Operations
    {
        /// <summary>
        /// Operation method, this can return null
        /// null here works as a default value
        /// </summary>
        /// <param name="numbA"></param>
        /// <param name="numbB"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public static float? Operation(float numbA, float numbB, int operationType)
        {
            switch (operationType)
            {
                case 1:
                    return numbA + numbB;
                case 2:
                    return numbA - numbB;
                case 3:
                    return numbA * numbB;
                case 4:
                    if (numbB == 0)
                        return null; // it's impossible to divide by zero so returns null
                    return numbA / numbB;
                default:
                    return null;
            }
        }

    }
}
