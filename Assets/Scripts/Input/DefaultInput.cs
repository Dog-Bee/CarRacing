using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultInput
{
    private DefaultInputControl _control;

    public InputAction Esc => _control.Default.Esc;

    public DefaultInput()
    {
        _control = new DefaultInputControl();
        _control.Enable();
    }
}
