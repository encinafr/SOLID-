using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace SingleResponsibilityPrinciple
{

    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; //memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string file, bool overwrite = false)
        {
            if (overwrite || !File.Exists(file))
                File.WriteAllText(file, j.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();

            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");

             WriteLine(j);

            var p = new Persistence();
            var filename = @"D:\WS\SOLID-\1-Single Responsibility Principle\SingleResponsibilityPrinciple\journal.txt";
            p.SaveToFile(j, filename, true);

            var pr= new Process();
            pr.StartInfo = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };
            pr.Start();

        }
    }
}
