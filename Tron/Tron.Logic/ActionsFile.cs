using System.IO;
using Tron.Logic.Interfaces;

namespace Tron.Logic
{
    public class ActionsFile : IActionsFile
    {
        private readonly string _fileName;
        public ActionsFile(string fileName)
        {
            _fileName = fileName;
        }

        public string Read()
        {
            return File.ReadAllText(_fileName);
        }
        
    }
}