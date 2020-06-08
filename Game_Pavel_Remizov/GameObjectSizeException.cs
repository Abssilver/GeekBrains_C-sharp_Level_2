using System.Drawing;

namespace Game_Pavel_Remizov
{
    class GameObjectSizeException : GameObjectException
    {
        public Size Size { get; }
        public GameObjectSizeException(string message, Size size) : base(message)
        {
            Size = size;
        }
        public GameObjectSizeException(Size size)
        {
            Size = size;
        }
    }
}
