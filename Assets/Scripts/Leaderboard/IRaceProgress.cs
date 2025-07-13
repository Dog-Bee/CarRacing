using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaceProgress
{
   public float TotalProgress { get; }
   public float TimeTrack { get; }
   public string Name { get; }
   public bool IsPlayer { get; }
   public bool IsStop { get; }
}
