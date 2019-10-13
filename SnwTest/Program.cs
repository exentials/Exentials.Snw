using Exentials.Snw.SnwConnector;
using System;

namespace SnwTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using SnwConnection conn = new SnwConnection("<host>", 0, "111", "username", "password", "E");
            conn.Open();
            Console.WriteLine($"KernelRelease: {conn.KernelRelease}");
            conn.Close();
            Console.ReadLine();
        }
    }
}
