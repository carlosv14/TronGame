namespace Tron.Logic
{
    public class Turn
    {
        public Player Player;
        public string Movement;

        public Turn(Player player, string movement)
        {
            Player = player;
            Movement = movement;
        }
    }
}