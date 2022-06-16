using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Lottery
{
    class Program
    {
        public static int attempts = 1600;
        public static double[] allAttempts = new double[attempts];
        public static string display = "";
        public static int togo = attempts;
        public static Stopwatch SW = new();
        static void Main(string[] args)
        {
            SW.Start();
            for (int i = 0; i < allAttempts.Length; i++)
            {
                int temp = i;
                Thread my_thread = new(() => startZero(temp));
                my_thread.Start();
            }
        }
        static void startZero(int index)
        {
            Console.WriteLine("Starting thread #" + index);
            double runs1 = 0;
            Random random = new();
            while (true)
            {
                runs1++;
                int coin1 = random.Next(0, 292201338);
                if (coin1 == 0)
                {
                    allAttempts[index] = runs1;
                    togo--;
                    if (togo == 0)
                    {
                        Array.Sort(allAttempts);
                        display = String.Join(Environment.NewLine, allAttempts);
                        Console.WriteLine(display);
                        SW.Stop();
                        Console.WriteLine(SW.Elapsed);
                        Console.WriteLine(allAttempts.Average());

                    }
                    else
                    {
                        Console.WriteLine("Thread #" + index + " finished after " + runs1 + " runs");
                    }
                    break;
                }
            }
        }
    }
}
