using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpindleController : MonoBehaviour
{
    public GameObject rightHandAnchor;
    public GameObject leftHandAnchor;

    private LineRenderer joinLineRenderer;

    private Vector3? pickedObjectInitialScale = null;
    private float? lineInitialMagnitude = null;

    //private Quaternion? pickedObjectInitialRotation = null;
    //private Vector3? lineInitialDirection = null;

    void Start()
    {
        joinLineRenderer = GetComponent<LineRenderer>();
    }

    void DrawLine()
    {
        joinLineRenderer.enabled = true;
        Vector3 R = rightHandAnchor.transform.position;
        Vector3 L = leftHandAnchor.transform.position;
        joinLineRenderer.SetPosition(0, R);
        joinLineRenderer.SetPosition(1, L);
    }

    void RemoveLine()
    {
        joinLineRenderer.enabled = false;
    }

    float GetLineMagnitude()
    {
        return GetLineDirection().magnitude;
    }

    Vector3 GetLineDirection()
    {
        Vector3 R = rightHandAnchor.transform.position;
        Vector3 L = leftHandAnchor.transform.position;
        return (R - L);
    }

    public void onManipulateEnter(GameObject pickedObject)
    {
        if (!pickedObjectInitialScale.HasValue)
        {
            pickedObjectInitialScale = pickedObject.transform.localScale;
            lineInitialMagnitude = GetLineMagnitude();
        }
        float lineMagnitude = GetLineMagnitude();
        float lineRatio = lineMagnitude / lineInitialMagnitude.Value;
        pickedObject.transform.localScale = pickedObjectInitialScale.Value * lineRatio;

        //if (!pickedObjectInitialRotation.HasValue)
        //{
        //    pickedObjectInitialRotation = pickedObject.transform.rotation;
        //    lineInitialDirection = GetLineDirection();
        //}
        //Vector3 currentLineDirection = GetLineDirection();
        //Vector3 perp = Vector3.Cross(lineInitialDirection.Value, currentLineDirection);
        //float angle = Vector3.Angle(lineInitialDirection.Value, currentLineDirection);
        //Quaternion newRotation = Quaternion.AngleAxis(angle, perp);
        //pickedObject.transform.rotation = pickedObjectInitialRotation.Value * newRotation;
        //pickedObject.transform.rotation = newRotation;

        DrawLine();
    }

    public void onManipulateExit()
    {
        pickedObjectInitialScale = null;
        lineInitialMagnitude = null;

        //lineInitialDirection = null;
        //pickedObjectInitialRotation = null;

        RemoveLine();
    }
}
