
namespace Game_Pavel_Remizov
{
    class GameObjectSpeedException : GameObjectException
    {
        public double Speed { get; }
        public GameObjectSpeedException(string message, double speed) : base(message)
        {
            Speed = speed;
        }
        public GameObjectSpeedException(double speed)
        {
            Speed = speed;
        }
    }
}
