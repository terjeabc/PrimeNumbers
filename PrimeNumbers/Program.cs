using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var n in GetPrimes2(1000000))
            {
                Console.WriteLine(n*n);
            }

            //MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            Console.WriteLine("Starting calculation.");
            var count = 1000000;
            var numbers = await GetPrimesAsync(count);
            Console.WriteLine("Found " + count + " primes...");
            //foreach (var prime in numbers)
            //{
            //    Console.Write(prime + " ");
            //}
        }

        static async Task<IEnumerable<int>> GetPrimesAsync(int count)
        {
            Console.WriteLine("Calculation started.");
            var task = Task.Run(() => GetPrimes(count));
            var numbers = await task;
            return numbers;

        }

        static IEnumerable<int> GetPrimes(int count)
        {
            var primes = new int[count];
            var index = 0;
            primes[index] = 2;
            while (index < count - 1)
            {
                var n = primes[index] + 1;
                while (!IsPrime(n, primes, index)) n++;
                primes[++index] = n;
                if(index%100000==0) Console.Write(".");
            }
            return primes;
        }

        static IEnumerable<int> GetPrimes2(int count)
        {
            var primes = new int[count];
            var index = 0;
            primes[index] = 2;
            yield return 2;
            while (index < count - 1)
            {
                var n = primes[index] + 1;
                while (!IsPrime(n, primes, index)) n++;
                yield return n;
                primes[++index] = n;
                if (index % 100000 == 0) Console.Write(".");
            }
            yield break;
        }

        static bool IsPrime(int number, int[] primes, int index)
        {
            var maxFactorIfNotPrime = Convert.ToInt32(Math.Ceiling(Math.Sqrt(number)));
            for (var i = 0; i < index && primes[i] <= maxFactorIfNotPrime; i++)
            {
                if (number % primes[i] == 0) return false;
            }
            return true;
        }
    }
}
