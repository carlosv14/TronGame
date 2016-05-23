using System.IO;
using Tron.Logic;

namespace Tron.Test
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