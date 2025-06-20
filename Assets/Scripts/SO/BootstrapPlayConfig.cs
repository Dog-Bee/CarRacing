using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BootstrapPlayConfig", menuName = "BootstrapPlayConfig/BootstrapPlayConfig")]
public class BootstrapPlayConfig : ScriptableObject
{
    [SceneName]
    public string sceneName;
}
