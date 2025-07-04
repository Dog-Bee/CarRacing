using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private List<Transform> targetList;

    public Vector3 GetTarget()
    {
        return targetList[Random.Range(0, targetList.Count)].position;
    }

    public void Activate()
    {
        targetList.ForEach(t=>t.gameObject.SetActive(true));
    }

    public void Deactivate()
    {
        targetList.ForEach(t=>t.gameObject.SetActive(false));
    }

    public Vector3 GetMiddlePosition()
    {
        return transform.position;
    }
}
