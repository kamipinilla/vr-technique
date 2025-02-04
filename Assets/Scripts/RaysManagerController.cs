using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaysManagerController : MonoBehaviour
{
    public GameObject rightHandAnchor;
    public GameObject leftHandAnchor;
    public GameObject interactionSphere;

    private LineRenderer rightLineRenderer;
    private LineRenderer leftLineRenderer;
    private LineRenderer joinLineRenderer;

    void Start()
    {
        joinLineRenderer = GetComponent<LineRenderer>();
        rightLineRenderer = rightHandAnchor.GetComponent<LineRenderer>();
        leftLineRenderer = leftHandAnchor.GetComponent<LineRenderer>();
    }

    void Update()
    {
        // L1
        Vector3 A = leftLineRenderer.GetPosition(0);
        //Vector3 A = new Vector3(0, 2, -1);

        Vector3 u = leftLineRenderer.GetPosition(1) - A;
        //Vector3 u = new Vector3(1, 1, 2);

        // L2
        Vector3 B = rightLineRenderer.GetPosition(0);
        //Vector3 B = new Vector3(1, 0, -1);

        Vector3 v = rightLineRenderer.GetPosition(1) - B;
        //Vector3 v = new Vector3(1, 1, 3);

        // Eq 1
        float e = Vector3.Dot(u, v);
        float f = -Vector3.Dot(u, u);
        float g = Vector3.Dot(u, A) - Vector3.Dot(u, B);

        // Eq 2
        float l = Vector3.Dot(v, v);
        float k = -Vector3.Dot(v, u);
        float m = Vector3.Dot(v, A) - Vector3.Dot(v, B);

        float s = (f*m - k*g) / (f*l - k*e);
        float t = (g - e*s) / f;

        Vector3 P = A + t*u;
        Vector3 Q = B + s*v;

        //joinLineRenderer.SetPosition(0, P);
        //joinLineRenderer.SetPosition(1, Q);

        Vector3 PIP = 0.5f * (P + Q);
        interactionSphere.transform.position = PIP;
    }

    public void ChangeComponentsVisibility(bool enable)
    {
        interactionSphere.GetComponent<Renderer>().enabled = enable;
        rightLineRenderer.enabled = enable;
        leftLineRenderer.enabled = enable;
        //joinLineRenderer.enabled = enable;
    }
}
