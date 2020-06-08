using System;

/*
*Создать собственное исключение GameObjectException, которое появляется 
при попытке создать объект с неправильными характеристиками
(например, отрицательные размеры, слишком большая скорость или позиция).
*/
namespace Game_Pavel_Remizov
{
    class GameObjectException: ArgumentException
    {
        public GameObjectException()
        { }
        public GameObjectException(string message) : base(message)
        { }
        public GameObjectException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
