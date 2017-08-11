using System;

namespace RsaEncryption
{
    /*
     * Entry point of the program, display information to user on console.
     */
    internal class Program
    {
        public static void Main(string[] args)
        {
            var primeGen = new Primes();
            var key = primeGen.GetKey();
            const string message = "Hello World!";
            Console.WriteLine("Original Text: \"" + message + "\"");
            var cypherText = key.Encrypt(message);
            Console.Write("Cypher Text: ");
            var isFirstLetter = true;
            foreach (var place in cypherText)
            {
                if (isFirstLetter)
                {
                    isFirstLetter = false;
                    Console.Write(place);
                    continue;
                }
                Console.Write(", " + place);
            }
            Console.WriteLine();
            var decryptedText = key.Decrypt(cypherText);
            Console.WriteLine("Decrypted Text: \"" + decryptedText + "\"");
        }
    }
}
