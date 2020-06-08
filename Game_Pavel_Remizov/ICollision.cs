using System;
using System.Drawing;

namespace Game_Pavel_Remizov
{
    //Сделать так, чтобы при столкновении пули с астероидом они регенерировались в разных концах экрана.
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
        void GenerateNewPosition(Random rnd);
    }
}
