using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour

{
    private Vector3 Gravity;
    private float Force, Mass, Drag;

    public void Init(Vector3 gravity, float force, float mass, float drag)
    {
        Gravity = gravity;
        Force = force;
        Mass = mass;
        Drag = drag;
    }
    public GameObject point;
    public List<GameObject> points;
    public int pointsNumber =10;

    private void Start()
    {
        for (int i = 0; i < pointsNumber; i++)
        {
            var p = Instantiate(point, Vector3.zero, Quaternion.identity);
            points.Add(p);
        }
    }
    /// <summary>
    /// Computes an array of Trajectory object positions by time.
    /// </summary>
    /// <returns>Number of positions filled into the buffer</returns>
    /// <param name="startPos">Starting Position</param>
    /// <param name="direction">Direction (and magnitude) vector</param>
    /// <param name="yFloor">Minimum height, below which is clipped</param>
    /// <param name="positions">Buffer to fill with positions</param>
    public void GetPositions(Vector3 startPos, Vector3 velocity, float yFloor/*, Vector3[] positions*/)
    {
        float timeIncrement = Time.fixedDeltaTime;

        int maxItems = 10;//positions.Length;
        int i = 0;

        //Vector3 velocity = direction * Force / Mass;
        Vector3 pos = startPos;
        for (; i < maxItems; i++)
        {
            velocity += Gravity * timeIncrement;
            velocity *= Mathf.Clamp01(1f - Drag * timeIncrement);
            pos += velocity * timeIncrement;
            if (pos.y < yFloor)
                break;
           // positions[i] = pos;
            points[i].gameObject.transform.position = pos;
        }
      //  return i;
    }
}