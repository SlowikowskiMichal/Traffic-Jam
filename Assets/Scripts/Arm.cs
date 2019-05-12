using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public Transform Target;
    public float startWidth = 1f;
    public float endWidth = 1f;
    public float downForce = 1f;
    private LineRenderer line;

    float lineStep;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        SetWidth();
        lineStep = 1f/(line.positionCount - 1f);
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        Vector3  middle = Vector3.Lerp(
            this.transform.position,
            Target.position,
            0.5f)+(Vector3.down*downForce);

        for(int i = 0; i < line.positionCount; i++)
        {
            float currentPoint = lineStep * i;
            Vector3 pointPosition = Vector3.Lerp(
                Vector3.Lerp(this.transform.position,middle,currentPoint),
                Vector3.Lerp(middle,Target.position,currentPoint),
                currentPoint);
            line.SetPosition(i,pointPosition);
        }
    }
    void SetWidth()
    {
        line.startWidth = startWidth;
        line.endWidth = endWidth;
    }
}
