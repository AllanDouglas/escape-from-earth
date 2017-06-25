namespace Interfaces
{
    public interface IMovable
    {
        UnityEngine.Vector2 ToPosition{ get; }
        bool Moving { get; }

        void MoveTo(UnityEngine.Vector2 position);        
        void StopMoviment();
    }
}
