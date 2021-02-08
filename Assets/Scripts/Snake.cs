using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _templateHead;
    [SerializeField] private SnakeBodyPart _templateBodyPart;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private float _speed;

    private Map _map;
    private Queue<SnakeBodyPart> _bodyParts;
    private SnakeHead _head;
    private bool _isNextCellWithFood = false;
    private Direction _lastDirection = Direction.Up;
    private Direction _direction = Direction.Up;
    private float _timer = 0;
    private bool _isDied = false;

    public event UnityAction Died;

    private void OnEnable()
    {
        _input.OccurredInput += ChangeDirectition;
    }

    private void OnDisable()
    {
        _input.OccurredInput -= ChangeDirectition;
    }

    private void Update()
    {
        if (_isDied == false)
        {
            _timer += Time.deltaTime;

            if (_timer > 1 / (_speed + _wallet.Score / 10f))
            {
                _timer = 0;
                Move();
            }
        }
    }

    public void Init(Map map)
    {
        _bodyParts = new Queue<SnakeBodyPart>();
        _map = map;

        _map.TryGetRandomActivePosition(out Vector2Int headPosition);
        Transform parentHead = _map.GetCellTransform(headPosition, out bool isThereFood);
        _head = Instantiate(_templateHead, parentHead);
        _head.SetParent(parentHead, headPosition);
    }

    private void Move()
    {
        Vector2Int nextPosition = GetNextPosition();
        Transform parent = _map.GetCellTransform(nextPosition, out bool isThereFood);

        if (parent == null)
        {
            _isDied = true;
            Died?.Invoke();
            return;
        }

        _lastDirection = _direction;

        if (_bodyParts.Count == 0)
        {
            if (_isNextCellWithFood)
            {
                SnakeBodyPart bodyPart = Instantiate(_templateBodyPart);
                bodyPart.Init(_head.ParentTranform, _head.ParentPosition, false);
                _bodyParts.Enqueue(bodyPart);
            }
            else
            {
                _map.ActivateCell(_head.ParentPosition);
            }
        }
        else
        {
            if (_isNextCellWithFood)
            {
                SnakeBodyPart bodyPart = Instantiate(_templateBodyPart);
                bodyPart.Init(_head.ParentTranform, _head.ParentPosition, _isNextCellWithFood);
                _bodyParts.Enqueue(bodyPart);
            }
            else
            {
                SnakeBodyPart bodyPart = _bodyParts.Dequeue();
                _map.ActivateCell(bodyPart.ParentPosition);
                bodyPart.Init(_head.ParentTranform, _head.ParentPosition, _isNextCellWithFood);
                _bodyParts.Enqueue(bodyPart);
            }
        }

        if (isThereFood)
            _wallet.AddOne();

        _isNextCellWithFood = isThereFood;
        _head.SetParent(parent, nextPosition);
    }


    private Vector2Int GetNextPosition()
    {
        Vector2Int position = _head.ParentPosition + new Vector2Int(0, 1);

        switch (_direction)
        {
            case Direction.Up:
                position = _head.ParentPosition + new Vector2Int(0, 1);
                break;
            case Direction.Down:
                position = _head.ParentPosition + new Vector2Int(0, -1);
                break;
            case Direction.Left:
                position = _head.ParentPosition + new Vector2Int(-1, 0);
                break;
            case Direction.Right:
                position = _head.ParentPosition + new Vector2Int(1, 0);
                break;
        }

        return position;
    }

    private void ChangeDirectition(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                if (_lastDirection == Direction.Down || _direction == Direction.Up)
                    return;
                break;
            case Direction.Down:
                if (_lastDirection == Direction.Up || _direction == Direction.Down)
                    return;
                break;
            case Direction.Left:
                if (_lastDirection == Direction.Right || _direction == Direction.Left)
                    return;
                break;
            case Direction.Right:
                if (_lastDirection == Direction.Left || _direction == Direction.Right)
                    return;
                break;
        }

        _direction = direction;
        _head.Rotate(direction);
    }
}
