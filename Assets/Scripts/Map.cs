using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    private Dictionary<Vector2Int, Cell> _cells;
    private Dictionary<Vector2Int, Cell> _activeCells;
    private Vector2Int _foodPosition;

    public Map (List<Cell> cells)
    {
        _cells = new Dictionary<Vector2Int, Cell>();
        _activeCells = new Dictionary<Vector2Int, Cell>();

        foreach (var cell in cells)
        {
            _cells[cell.Position] = cell;

            if (cell.IsBorder == false)
                _activeCells[cell.Position] = cell;
        }

        EnableRandomFood();
    }

    public Transform GetCellTransform(Vector2Int position, out bool isThereFood)
    {
        isThereFood = false;

        if (_activeCells.ContainsKey(position))
        {
            Cell cell = _activeCells[position];

            if (_foodPosition == position)
            {
                isThereFood = true;
                DisableFood();
                EnableRandomFood();
            }

            _activeCells.Remove(position);
            return cell.transform;
        }

        return null;
    }

    public void ActivateCell (Vector2Int position)
    {
        if (_cells.ContainsKey(position))
            _activeCells[position] = _cells[position];
    }

    public bool TryGetRandomActivePosition(out Vector2Int position)
    {
        int index = Random.Range(0, _activeCells.Count);
        position = new Vector2Int();

        foreach (var activePosition in _activeCells.Keys)
        {
            if (index == 0)
            {
                position = activePosition;
                return true;
            }

            index--;
        }

        return false;
    }

    private void EnableRandomFood()
    {
        if(TryGetRandomActivePosition(out Vector2Int position))
        {
            _activeCells[position].EnableFood();
            _foodPosition = position;
        }
    }

    private void DisableFood()
    {
        if (_cells.ContainsKey(_foodPosition))
            _cells[_foodPosition].DisableFood();
    }
}
