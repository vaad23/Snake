using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Cell _template;

    public Map Create(int width, int height)
    {
        List<Cell> cells = new List<Cell>();

        width = Mathf.Clamp(width + 2, 10, 40);
        height = Mathf.Clamp(height + 2, 5, 20);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = Instantiate(_template, transform);
                bool isBorder = i == 0 || j == 0 || i == width - 1 || j == height - 1;

                cell.Init(new Vector2Int(i, j), isBorder);
                cells.Add(cell);
            }
        }

        transform.position -= new Vector3(width / 2f - 0.5f, height / 2f - 0.5f);

        return new Map(cells);
    }
}
