using System.Collections.Generic;
using System.Linq;

namespace Tron.Logic
{
    public class Player
    {
        public string Name;
        public Coordinates Coordinates;
        private readonly List<Coordinates> _path;
       
      
        public Player(string name, Coordinates coordinates)
        {
            Name = name;
            Coordinates = coordinates;
            _path = new List<Coordinates> {coordinates};
        }

        public void AddCoordinateToPath(Coordinates coordinate)
        {
          _path.Add(coordinate);  
        }

        public Coordinates GetLastCoordinate()
        {
            return _path.Last();
        }
        public Player FindCoordinateInPath(Coordinates coordinates)
        {
            foreach (var coordinate in _path)
            {
                if(coordinate.XPosition == coordinates.XPosition &&
                    coordinate.YPosition == coordinates.YPosition)
                    return this;
            }
            return null;
        } 
    }
}