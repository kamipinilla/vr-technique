using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIPSphereController : MonoBehaviour
{
    public Material PIPActive;
    public Material PIPInactive;
    public SpindleController spindleController;
    public RaysManagerController raysManagerController;

    private GameObject pickable;
    private GameObject scaledObject;
    private new Renderer renderer;

    private bool performingScale;

    void Start()
    {
        pickable = null;
        renderer = GetComponent<Renderer>();
        performingScale = false;
    }

    void Update()
    {
        bool one = OVRInput.Get(OVRInput.Button.One);
        bool two = OVRInput.Get(OVRInput.Button.Two);
        bool three = OVRInput.Get(OVRInput.Button.Three);
        bool four = OVRInput.Get(OVRInput.Button.Four);
        bool pickAction = one || three;
        bool scaleAction = two || four;
        if (pickAction && pickable != null)
        {
            AnchorToPickable();
        }
        if (scaleAction)
        {
            if (scaledObject == null)
            {
                if (pickable != null)
                {
                    scaledObject = pickable;
                }
            }
            else
            {
                spindleController.onManipulateEnter(scaledObject);
                performingScale = true;
                if (!pickAction)
                {
                    raysManagerController.ChangeComponentsVisibility(false);
                }
            }
        }
        else if (performingScale)
        {
            scaledObject = null;
            spindleController.onManipulateExit();
            performingScale = false;
            raysManagerController.ChangeComponentsVisibility(true);
        }
    }

    void AnchorToPickable()
    {
        pickable.transform.position = transform.position;
    }

    void ChangePipLook(bool active)
    {
        Material newMaterial = active ? PIPActive : PIPInactive;
        Material[] materials = renderer.materials;
        materials[0] = newMaterial;
        renderer.materials = materials;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.CompareTag("Pickable"))
        {
            pickable = gameObject;
            ChangePipLook(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.CompareTag("Pickable"))
        {
            pickable = null;
            ChangePipLook(false);
        }
    }
}
