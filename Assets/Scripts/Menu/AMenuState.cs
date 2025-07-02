using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AMenuState : MonoBehaviour,IMenuState
{
    protected SignalBus _signalBus;
    public virtual void SendState()
    {
        _signalBus.Fire(new MenuStateChangeSignal(this));
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void EnterOverlap();
    public abstract void ExitOverlap();
    public abstract void EnterImmediately();
    public abstract void ExitImmediately();
}
