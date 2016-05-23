using System.Collections.Generic;

namespace Tron.Logic.Interfaces
{
    public interface IParser
    {
        List<string> PlayersFromFile();
        List<string> TurnsFromFile();
    }
}