using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Material activeMaterial;
    public Material inactiveMaterial;

    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void ChangeLook(bool active)
    {
        Material newMaterial = active ? activeMaterial : inactiveMaterial;
        Material[] materials = renderer.materials;
        materials[0] = newMaterial;
        renderer.materials = materials;
    }

    public void OnTargetMatch()
    {
        ChangeLook(true);
    }

    public void OnTargetUnmatch()
    {
        ChangeLook(false);
    }
}
