using System;
using System.IO;
using CsvHelper;
using System.Collections;

namespace RuntimeComponent
{
    public sealed class Class1
    {
        public static bool ExportData(IList data, String path)
        {
            try
            {
                using (TextWriter textWriter = File.CreateText(path))
                {
                    CsvWriter csvWriter = new CsvWriter(textWriter);
                    csvWriter.WriteRecords(data);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
