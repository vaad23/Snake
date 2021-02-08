using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public Vector2Int ParentPosition { get; private set; }
    public Transform ParentTranform { get; private set; }

    public void SetParent(Transform parent, Vector2Int parentPosition)
    {
        ParentPosition = parentPosition;
        ParentTranform = parent;
        transform.SetParent(parent, false);
    }

    public void Rotate(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }
}
