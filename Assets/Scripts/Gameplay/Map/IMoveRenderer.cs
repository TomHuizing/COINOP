using UnityEngine;

public interface IMoveRenderer
{
    public void MoveTo(Vector2 targetPosition);
    public void Teleport(Vector2 targetPosition);
}
