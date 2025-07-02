using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateChangeSignal
{
    public readonly IMenuState MenuState;

    public MenuStateChangeSignal(IMenuState menuState)
    {
        MenuState = menuState;
    }
}
