using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RacePathService
{
    private readonly List<Vector3> _points;

    private readonly float[] _accumulatedDistances;

    public float TotalLength { get; private set; }

    public RacePathService(List<Vector3> points)
    {
        _points = points;
        _accumulatedDistances = new float[_points.Count];
        float total = 0f;
        
        total += Vector3.Distance(_points[^1], _points[0]);
        _accumulatedDistances[0] = total;
        Debug.Log($"Accumulated distances index 0: {_accumulatedDistances[0]}");
        for (int i = 1; i < _points.Count; i++)
        {
            total += Vector3.Distance(_points[i-1], _points[i]);
            _accumulatedDistances[i] = total;
            Debug.Log($"Accumulated distances index {i}: {_accumulatedDistances[i]}");
        }
        TotalLength = total;
        Debug.Log("Total length: " + TotalLength);
        
    }

    public float GetProgressAlongPath(Vector3 position, int index)
    {
        int count = _points.Count;
        int previousIndex = (index-1+count)%count;
        
        Vector3 start = _points[previousIndex];
        Vector3 end = _points[index];
        
        Vector3 projected = ProjectPointOnSegment(start, end, position);
        
        float distanceOnSegment = Vector3.Distance(start, projected);
        float distanceToStart = _accumulatedDistances[previousIndex];
     

        if(index==0)
            return distanceOnSegment;
       
        return distanceToStart+distanceOnSegment;
    }


    private Vector3 ProjectPointOnSegment(Vector3 startPos, Vector3 endPos, Vector3 currentPos)
    {
        Vector3 cs= currentPos - startPos;
        Vector3 es = endPos - startPos;
        float dot = Mathf.Clamp01(Vector3.Dot(cs, es) / es.sqrMagnitude);
        
        return startPos + dot * es;
    }
}
