using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Common.DongleHelper
{
    public class Dongle
    {
        public static void Check(string key, bool trial)
        {
            try
            {
                new Dog().CheckDog(key, trial);
            }
            catch
            {
                throw new Exception("Because you did not buy full authorities of the trial part production, only few times trial released, now all the trial permission has expired. Please contact the conventor to buy all the full authorization, otherwise no trial authentications granted anymore.");
            }
        }
    }
}
