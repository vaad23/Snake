using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    private Vector3 _bigScale = new Vector3(0.9f, 0.9f, 1);
    private Vector3 _smallScale = new Vector3(0.7f, 0.7f, 1);

    public Vector2Int ParentPosition { get; private set; }

    public void Init(Transform parent, Vector2Int parentPosition, bool isFullness)
    {
        transform.SetParent(parent, false);
        gameObject.SetActive(true);
        ParentPosition = parentPosition;

        Vector3 scale = isFullness ? _bigScale : _smallScale;
        transform.localScale = scale;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
