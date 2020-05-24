using System;
using System.Collections.Generic;
using System.IO;

namespace merge
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2 && args[0].EndsWith(".csv") && args[1].EndsWith(".csv"))
            {
                var firstPath = Directory.GetCurrentDirectory() + "\\" + args[0];
                var updatePath = Directory.GetCurrentDirectory() + "\\" + args[1];
                var csvHandler = new CsvHandler(firstPath, updatePath);
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + "output.csv", csvHandler.MergeCsv());
            }
            else
            {
                Console.WriteLine("WRONG INPUT ARGUMENTS \nEXAMPLE OF USAGE: merge.exe sales.csv new_sales.csv");
            }
        }
    }
}
