using System;
using System.Collections;

namespace MyEmployeesFinance.PluginInterface
{
    public interface IPlugin
    {
        string Name { get; }
        string GetDescription();
        double GetLastResult { get; }
        bool Execute(IList data, string filePath);
        event EventHandler OnExecute;
        void ExceptionTest(string input);
    }
}
