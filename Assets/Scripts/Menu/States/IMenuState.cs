using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuState
{
    public void SendState();
    public void EnterState();
    public void ExitState();
    public void EnterOverlap();
    public void ExitOverlap();
    public void EnterImmediately();
    public void ExitImmediately();
}
