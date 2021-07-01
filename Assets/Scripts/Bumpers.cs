using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(LineRenderer))]
public class Bumpers : MonoBehaviour
{
    LineRenderer line;
    public Transform[] bumpers;
    private int curveCount = 0;
    private int SEGMENT_COUNT = 50;
    private int count;

    void Start()
    {

        if (!line)
        {
            line = GetComponent<LineRenderer>();
        }

        count = transform.childCount;
        bumpers = new Transform[count];
        for (int i = 0; i < transform.childCount; i++)
        {
            bumpers[i] = transform.GetChild(i);
            bumpers[i].GetComponentInChildren<Bumper>().SetIndex(count);
            count--;
        }

        line.positionCount = bumpers.Length;
        curveCount = (int) bumpers.Length / 3;

        for (int i = 0; i < bumpers.Length; i++)
        {
            line.SetPosition(i, bumpers[i].GetComponent<BoxCollider>().bounds.center);
        }

        //DrawCurve();

    }

    void DrawCurve()
    {
        for (int j = 0; j < curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, bumpers[nodeIndex].GetComponent<BoxCollider>().bounds.center, bumpers[nodeIndex + 1].GetComponent<BoxCollider>().bounds.center, bumpers[nodeIndex + 2].position, bumpers[nodeIndex + 3].GetComponent<BoxCollider>().bounds.center);
                line.positionCount = (j * SEGMENT_COUNT) + i;
                line.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
            }

        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

}


