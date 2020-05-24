using System;
using System.Collections.Generic;
using System.IO;

namespace merge
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstPath = @"C:\Repos\CSVmerge\sales.csv";
            var updatePath = @"C:\Repos\CSVmerge\new_sales.csv";
            var firstFile = FileReader(firstPath);
            var updateFile = FileReader(updatePath);
            var firstFileArray  = SingleToDoubleDimension(HeaderCounter(firstPath), firstFile.ToArray());
            var updateFileArray = SingleToDoubleDimension(HeaderCounter(updatePath), updateFile.ToArray());
            var mergedArray = MergeArrays(firstFileArray, updateFileArray);
            var finalString = ArrayToString(mergedArray);
            File.WriteAllText(@"C:\Repos\CSVmerge\output.csv", finalString);
        }

        private static List<string> FileReader(string path)
        {
            List<string> list = new List<string>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        list.Add(value);
                    }
                }
            }
            return list;
        }

        private static int HeaderCounter(string path)
        {
            int i = 0;
            using (var reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        i++;
                    }
            }
            return i;
        }
        private static string[,] SingleToDoubleDimension(int divider, string []array)
        {
            string[,] newArray = new String[array.Length/divider,divider];
            var counter = 0;
            for (int i = 0; i < array.Length / divider; i++)
            {
                for (int j = 0; j < divider; j++)
                {
                    newArray[i, j] = array[counter];
                    counter++;
                }
            }
            return newArray;
        }

        private static string[,] MergeArrays(string [,]array1, string[,] array2)
        {
            var counter = 0;
            for (int i = 0; i < array1.GetLength(1); i++)
            {
                for (int j = 0; j < array2.GetLength(1); j++)
                {
                    if (array1[0, i] == array2[0, j])
                    {
                     counter++;
                    }
                }
                
            }
            string[,] newArray = new String[array1.GetLength(0)+ array2.GetLength(0) - 1, array1.GetLength(1)+ array2.GetLength(1) - counter];

            int z = 0;
            for (int i = 0; i < array1.GetLength(1); i++)
            {
                newArray[0, i] = array1[0, i];
            }
            for (int i = 0; i < array2.GetLength(1); i++)
            {
                int k = 0;
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    if (array2[0, i] == newArray[0, j])
                    {
                        k++;
                    }
                }

                if (k == 0)
                { 
                    newArray[0, array1.GetLength(1) + z] = array2[0, i];
                    for (int j = 1; j < array2.GetLength(0); j++)
                    {
                        newArray[j + array1.GetLength(0) - 1, array1.GetLength(1) + z] = array2[j, i];
                    }
                    z++;
                }

            }

            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    newArray[i, j] = array1[i, j];
                } 
            }
            for (int i = 1; i < array2.GetLength(0); i++)
            {
                for (int j = 0; j < counter; j++)
                {
                    newArray[i + array1.GetLength(0) - 1, j] = array2[i, j];
                }
            }
            return newArray;
        }

        private static string ArrayToString(string [,]array)
        { 
            var finalString = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != null)
                    { 
                        finalString = finalString + array[i, j] + ",";
                    }
                    else
                    {
                        finalString = finalString + "" + ",";
                    }
                }
                finalString = finalString.TrimEnd(',');
                finalString = finalString + Environment.NewLine;
            }
            return finalString;
        }
    }
}
