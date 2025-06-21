using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private Vector2 _move;
    public float Throttle => _move.y;
    public float Steering => _move.x;
    public bool IsDrifting => _controls.Move.Drift.IsPressed();

    private PlayerControl _controls;

    public PlayerInput()
    {
        _controls = new PlayerControl();
        _controls.Move.MoveVector.performed += ctx => _move = ctx.ReadValue<Vector2>();
        _controls.Move.MoveVector.canceled += ctx => _move = Vector2.zero;
        _controls.Enable();
    }
}
