using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Food _food;

    public Vector2Int Position { get; private set; }
    public bool IsBorder { get; private set; }

    public void Init(Vector2Int position, bool isBorder)
    {
        Position = position;
        IsBorder = isBorder;

        transform.position = new Vector3(position.x, position.y);
        if (isBorder)
            _renderer.color = Color.red;
    }

    public void EnableFood()
    {
        _food.Enable();
    }

    public void DisableFood()
    {
        _food.Disable();
    }
}