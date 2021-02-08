using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction<Direction> OccurredInput;

    private void Update()
    {
        if (Input.anyKeyDown)
            InputValidation();
    }

    private void InputValidation()
    {
        if (Input.GetKeyDown(KeyCode.W))
            OccurredInput?.Invoke(Direction.Up);

        if (Input.GetKeyDown(KeyCode.S))
            OccurredInput?.Invoke(Direction.Down);

        if (Input.GetKeyDown(KeyCode.A))
            OccurredInput?.Invoke(Direction.Left);

        if (Input.GetKeyDown(KeyCode.D))
            OccurredInput?.Invoke(Direction.Right);
    }
}
