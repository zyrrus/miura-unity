using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugIntersection : MonoBehaviour
{
    public Vector3 c1;
    public Vector3 c2;
    public Vector3 c3;

    public float r1;
    public float r2;
    public float r3;

    public bool showSpheres;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(c1, 0.1f);
        Gizmos.DrawSphere(c2, 0.1f);
        Gizmos.DrawSphere(c3, 0.1f);

        if (showSpheres)
        {
            Gizmos.color = new Color(0, 0, 0, 0.1f);
            Gizmos.DrawSphere(c1, r1);
            Gizmos.DrawSphere(c2, r2);
            Gizmos.DrawSphere(c3, r3);
        }
    }
}
