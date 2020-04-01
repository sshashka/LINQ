using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] nums = new int []{ 1 , 2, 3, -3 };
            var selectedTeams = (from t in nums
                                 where t < 0
                                 select t).Count();
            var Sum = (from t in nums
                                 where t < 0
                                 select t).Sum();
            if (Sum == 0)
                Console.WriteLine("00");
            Console.WriteLine(selectedTeams);
            Console.WriteLine(Sum);
            Console.ReadKey();
        }
    }
}
