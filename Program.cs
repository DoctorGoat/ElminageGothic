using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream elminage = File.Open("ULJM06091.bin", FileMode.Open);
            StreamReader elmin = new StreamReader(elminage, Encoding.GetEncoding(932));
            string TheEntireDamnGame = elmin.ReadToEnd();
            Console.WriteLine(TheEntireDamnGame.Length);
//            Regex r = new Regex("/守られている/g");
            MatchCollection results = Regex.Matches(TheEntireDamnGame, "[ぁ-黑 ]{3,1000}");
            Console.WriteLine(results.Count);
            FileStream outputFile = new FileStream("output.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(outputFile, Encoding.GetEncoding(932));
            StringBuilder outputBuilder = new StringBuilder();
            foreach (Match m in results)
            {
                outputBuilder.Clear();
                outputBuilder.Append(m.Index + "," + m.Value);
                write.WriteLine(outputBuilder);
             }
            write.Close();
            Console.ReadLine();
        }
    }
}
