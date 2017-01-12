using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace scriptdumpv2
{
    class Program
    {
        static void Main(string[] args)
        {
            //grab every file in the directory
            string[] fileList = Directory.GetFiles(Environment.CurrentDirectory);
            //iterate
            foreach(var file in fileList)
            {
                //we only want to go over the SCD files right now. We could also go over bin files later
                if(file.Contains("SCD") && !file.Contains(".csv"))
                {
                    FileStream toProcess = new FileStream(file, FileMode.Open);
                    //codepage 932 is effectively shift-jis
                    StreamReader fileText = new StreamReader(toProcess, Encoding.GetEncoding(932));
                    //read it all into memory
                    string theEntireFile = fileText.ReadToEnd();
                    //get each result that is valid japanese, plus a few other characters that are likely to be used
                    MatchCollection results = Regex.Matches(theEntireFile, "[,.;:\"A-z\u3000-\u30ff\uff00-\uffef\u4e00-\u9faf]{3,10000}");
                    //set up our output: we want a csv output file for google docs translation as of right now
                    FileStream toOutput = new FileStream(file.Substring(0, file.Length - 4) + ".csv", FileMode.Create);
                    StreamWriter fileWrite = new StreamWriter(toOutput, Encoding.Unicode);
                    //stringbuilder for setting up our output 
                    StringBuilder matchWrite = new StringBuilder();
                    foreach(Match m in results)
                    {
                        matchWrite.Clear();
                        //first, the index in the file of the result
                        matchWrite.Append(m.Index);
                        //then, the value. note the quotation marks: these are somewhat necessary to prevent the csv from detecting commas within the text from causing issues
                        matchWrite.Append(", \"");
                        matchWrite.Append(m.Value + "\"");
                        fileWrite.WriteLine(matchWrite);
                    }
                    //clean up
                    fileWrite.Close();
                    toProcess.Close();
                    toOutput.Close();
                }
            }
        }
    }
}
