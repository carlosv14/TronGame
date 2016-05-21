namespace Tron.Logic
{
    public class Player
    {
        public string Name;
        public Coordinates Coordinates;

        public Player(string name, Coordinates coordinates)
        {
            Name = name;
            Coordinates = coordinates;
        }
    }
}