using System;
using System.Numerics;
using System.Collections.Generic;

namespace RsaEncryption
{
    /*
     * Create a key using two primes. The variable n is both primes multiplied, phi is each prime minus one multiplied
     * together. The variable e has to satisfy gcd(e, phi) = 1, so just another prime. The variable d has to satisfy
     * ed = 1 mod phi. The public key is (e, n) and the private key is (d, n).
     */
    public class Key
    {
        private readonly int n;
        private readonly int e;
        private int d;

        public Key(int primeOne, int primeTwo, List<int> primes)
        {
            n = primeOne * primeTwo;
            var phi = (primeOne - 1) * (primeTwo - 1);
            var end = primes.Count - 1;
            var start = end / 4;
            var random = new Random();
            do
            {
                do
                {
                    e = primes[random.Next(start, end)];
                } while (e == primeOne || e == primeTwo);
            } while (!IsFoundD(phi));
            Console.WriteLine("Public Key: (e, n) = (" + e + ", " + n + ")");
        }

        public bool IsFoundD(int phi)
        {
            for (var i = phi - 1; i > 1; i--)
            {
                var mul = BigInteger.Multiply(e, i);
                var result = BigInteger.Remainder(mul, phi);
                if (result.Equals(1))
                {
                    d = i;
                    Console.WriteLine("Private Key: (d, n) = (" + d + ", " + n + ")");
                    return true;
                }
            }
            return false;
        }

        public int[] Encrypt(string message)
        {
            char[] charArray = message.ToCharArray();
            int[] array = new int[charArray.Length];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = (int) BigInteger.ModPow(charArray[i], e, n);
            }
            return array;
        }

        public string Decrypt(int[] cyphertext)
        {
            char[] array = new char[cyphertext.Length];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = (char) BigInteger.ModPow(cyphertext[i], d, n);
            }
            return new string(array);
        }
    }
}
