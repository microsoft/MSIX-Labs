using CsvHelper;
using MyEmployees.PluginInterface;
using System;
using System.Collections;
using System.IO;

namespace ExportDataLibrary
{
    public class ExportData: IPlugin
    {
        public string Name => throw new NotImplementedException();
        public double GetLastResult => throw new NotImplementedException();
        public event EventHandler OnExecute;

        public void ExceptionTest(string input)
        {
            throw new NotImplementedException();
        }

        public bool Execute(IList data, string filePath)
        {
            try
            {
                using (TextWriter textWriter = File.CreateText(filePath))
                {
                    CsvWriter csvWriter = new CsvWriter(textWriter);
                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.WriteRecords(data);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }
    }
}
