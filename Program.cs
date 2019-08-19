using SortAndSweepTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SortAndSweep
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SASItem> entites = new List<SASItem>();


            Random rand = new Random();

            Func<double> nextRand = ( () => rand.NextDouble() * 1000 - 500);

            for(int i = 0; i < 500000; i++)
            {
                double minValue = nextRand();
                double maxValue = nextRand();

                if(minValue > maxValue)
                {
                    var temp = maxValue;
                    maxValue = minValue;
                    minValue = temp;
                }

                entites.AddRange(SASItem.CreateItem(minValue, maxValue));
            }
            

            List<SASItem> xSorted = entites.OrderBy(x => x.Value).ToList();
            
            
            Dictionary<int, SASItem> activeItems = new Dictionary<int, SASItem>((int)(entites.Capacity * 0.7));
            List<SASItem> collidingItems = new List<SASItem>();
            

            Stopwatch sw = new Stopwatch();

            sw.Start();

            foreach (var item in xSorted)
            {
                if(item.Type == MarkerType.Min)
                {
                    //Console.WriteLine("Added : " + item.Value);
                    activeItems.Add(item.Id, item);
                }
                else
                {
                    //Console.WriteLine("Removed : " + item.MinItem.Value + ", " + item.Value);
                    activeItems.Remove(item.Id);
                }
            }

            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalMilliseconds);

            Console.ReadKey();
        }
    }
}
