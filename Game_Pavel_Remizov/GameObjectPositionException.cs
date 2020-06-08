using System.Drawing;

namespace Game_Pavel_Remizov
{
    class GameObjectPositionException : GameObjectException
    {
        public Point Position { get; }
        public GameObjectPositionException(string message, Point point) : base(message)
        {
            Position = point;
        }
        public GameObjectPositionException(Point point)
        {
            Position = point;
        }
    }
}
