using System.Collections.Generic;

namespace Tron.Logic
{
    public interface IParser
    {
        List<string> PlayersFromFile();
        List<string> TurnsFromFile();
    }
}