global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Timers;

namespace CollectionsStringsFiles
{
    internal class Program
    {
        private static string ApplyCommands(string[] commands)
        {
            var newStr = new StringBuilder();
            foreach(var line in commands)
            {
                var comm = MySplit(line);
                if (comm[0] == "push")
                {
                    newStr.Append(comm[1]);
                }
                else if (comm[0] == "pop")
                {
                    var symbolsToRemove = int.Parse(comm[1]);
                    newStr.Remove(newStr.Length - symbolsToRemove, symbolsToRemove);
                }
            }
            return newStr.ToString();
        }

        public static string[] MySplit(string text)
        {
            if (text[1] == 'o')
            {
                var str1 = "pop";
                var str2 = text.Substring(4);
                return new string[2] { str1, str2 };
            }
            else if (text[1] == 'u')
            {
                var str1 = "push";
                var str2 = text.Substring(5);
                return new string[2] { str1, str2 };
            }
            return new string[2];
        }

        public static void Main()
        {
            File.WriteAllText("C:\test\new.txt", "123456");
        }
    }
}