using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        CreateRay();
    }

    private void CreateRay()
    {
        float lengthScaleFactor = 1000f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.forward * lengthScaleFactor;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
