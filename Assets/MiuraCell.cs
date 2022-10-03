using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MiuraCell : MonoBehaviour
{
    public float r;
    // private float height = r * sqrt(3)/2;
    public Transform pointNW;
    public Transform pointNE;
    public Transform pointSW;
    public Transform pointSE;

    private Vector3 C;
    private Vector3 N;
    private Vector3 E;
    private Vector3 S;
    private Vector3 W;


    private void OnDrawGizmos()
    {
        Vector3 NW = pointNW.position;
        Vector3 NE = pointNE.position;
        Vector3 SW = pointSW.position;
        Vector3 SE = pointSE.position;

        float rRoot3 = r * (float)Math.Sqrt(3);

        // Draw corner points
        Gizmos.color = new Color(0, 0, 0);

        Gizmos.DrawSphere(NW, 0.1f);
        Gizmos.DrawSphere(NE, 0.1f);
        Gizmos.DrawSphere(SW, 0.1f);
        Gizmos.DrawSphere(SE, 0.1f);



        Gizmos.color = new Color(0, 0, 1); // BLUE
        C = TryDrawIntersection(NW, r, NE, r, SW, rRoot3, true);
        Gizmos.color = new Color(0, 1, 0); // GREEN
        N = TryDrawIntersection(NW, r, NE, r, C, r, false);
        Gizmos.color = new Color(0, 1, 1); // YELLOW
        E = TryDrawIntersection(NE, r, SE, r, C, r, true);
        Gizmos.color = new Color(1, 0, 0); // RED
        S = TryDrawIntersection(SW, r, SE, r, C, r, false);
        Gizmos.color = new Color(1, 0, 1); // PURPLE
        W = TryDrawIntersection(SW, r, NW, r, C, r, true);


        Gizmos.color = new Color(0, 0, 1, 0.5f);

        Gizmos.DrawLine(N, NE);
        Gizmos.DrawLine(N, NW);
        Gizmos.DrawLine(N, C);

        Gizmos.DrawLine(NW, W);
        Gizmos.DrawLine(NW, C);

        Gizmos.DrawLine(NE, E);
        Gizmos.DrawLine(NE, C);

        Gizmos.DrawLine(C, S);

        Gizmos.DrawLine(W, C);
        Gizmos.DrawLine(W, S);
        Gizmos.DrawLine(W, SW);

        Gizmos.DrawLine(E, C);
        Gizmos.DrawLine(E, S);
        Gizmos.DrawLine(E, SE);

        Gizmos.DrawLine(S, SE);
        Gizmos.DrawLine(S, SW);


        // Gizmos.DrawSphere(NW, r);
        // Gizmos.DrawSphere(NE, r);
        // Gizmos.DrawSphere(SW, r);
        // Gizmos.DrawSphere(SE, r);

        // Gizmos.DrawSphere(NW, rRoot3);
        // Gizmos.DrawSphere(NE, rRoot3);
        // Gizmos.DrawSphere(SW, rRoot3);
        // Gizmos.DrawSphere(SE, rRoot3);
    }

    private Vector3 TryDrawIntersection(Vector3 c1, float r1, Vector3 c2, float r2, Vector3 c3, float r3, bool useTop, bool showSpheres = false)
    {
        try
        {
            var (a, b) = IntersectThreeSpheres(c1, r1, c2, r2, c3, r3);

            if (showSpheres)
            {
                Gizmos.DrawSphere(c1, r1);
                Gizmos.DrawSphere(c2, r2);
                Gizmos.DrawSphere(c3, r3);
            }

            Vector3 desiredPoint;

            if (useTop)
            {
                desiredPoint = (a.y > b.y) ? a : b;
            }
            else
            {
                desiredPoint = (a.y > b.y) ? b : a;
            }

            Gizmos.DrawSphere(desiredPoint, 0.1f);
            return desiredPoint;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return new Vector3();
        }
    }

    private (Vector3, Vector3) IntersectThreeSpheres(Vector3 c1, float r1, Vector3 c2, float r2, Vector3 c3, float r3)
    {
        Vector3 temp1 = c2 - c1;
        Vector3 e_x = temp1 / temp1.magnitude;
        Vector3 temp2 = c3 - c1;
        float i = Vector3.Dot(e_x, temp2);
        Vector3 temp3 = temp2 - (i * e_x);
        Vector3 e_y = temp3 / temp3.magnitude;
        Vector3 e_z = Vector3.Cross(e_x, e_y);
        float d = temp1.magnitude;
        float j = Vector3.Dot(e_y, temp2);
        float x = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
        float y = (r1 * r1 - r3 * r3 - 2 * i * x + i * i + j * j) / (2 * j);
        float temp4 = r1 * r1 - x * x - y * y;
        if (temp4 < 0)
            throw new Exception("The three spheres do not intersect!");
        float z = (float)(Math.Sqrt(temp4));
        Vector3 p_12_a = c1 + x * e_x + y * e_y + z * e_z;
        Vector3 p_12_b = c1 + x * e_x + y * e_y - z * e_z;
        return (p_12_a, p_12_b);
    }

    private Vector3 FindNormalDirection(Vector3 a, Vector3 b)
    {
        throw new NotImplementedException();
    }
}
