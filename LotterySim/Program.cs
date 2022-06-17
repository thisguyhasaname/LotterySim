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
        public static int activeThreadCount = 0;
        public static int totalThreadCount = 0;
        public static int attempts = 10;
        public static double[] allAttempts = new double[attempts];
        public static string display = "";
        public static int togo = attempts;
        public static Stopwatch SW = new();
        
        static void Main(string[] args)
        {
            SW.Start();
            while (totalThreadCount < attempts)
            {
                if (activeThreadCount < 32)
                {
                    Thread my_thread = new(() => StartZero(totalThreadCount));
                    my_thread.Start();
                    totalThreadCount++;
                    activeThreadCount++;
                }
            }
        }
        static void StartZero(int index)
        {
            Console.WriteLine("Starting thread #" + index + " " + activeThreadCount + " threads are active");
            double runs1 = 0;
            Random random = new();
            while (true)
            {
                runs1++;
                int coin1 = random.Next(0, 292201338);
                if (coin1 == 0)
                {
                    Console.WriteLine("adding " + runs1 + " to allattempts[" + index + "]");
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
                    activeThreadCount--;
                    break;
                }
            }
        }
    }
}