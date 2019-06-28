using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.IO;

using Amazon.CostExplorer.Model;

namespace Sandbox.General
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            foreach (string arg in args)
                Console.WriteLine($">> [{arg}]");
        }
    }
}
