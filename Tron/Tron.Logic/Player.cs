namespace Tron.Logic
{
    public class Player
    {
        public string Name;
        public int PositionX, PositionY;

        public Player(string name, int positionY, int positionX)
        {
            Name = name;
            PositionY = positionY;
            PositionX = positionX;
        }
    }
}