using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Highlights : MonoBehaviour
{

    public Material highlightMaterial;
    Material originalMaterial;
    GameObject lastHighlightedObject;

    void HighlightObject(GameObject gameObject)
    {
        if (lastHighlightedObject != gameObject)
        {
            ClearHighlighted();
            originalMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = highlightMaterial;
            lastHighlightedObject = gameObject;
        }

    }

    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            lastHighlightedObject.GetComponent<MeshRenderer>().sharedMaterial = originalMaterial;
            lastHighlightedObject = null;
        }
    }

    void HighlightObjectInCenterOfCam()
    {
        float rayDistance = 1000.0f;
        // Ray from the center of the viewport.
        Ray ray = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, 1));
        RaycastHit rayHit;
        
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, 1000.0f))
        {
            // Get the object that was hit.
            GameObject hitObject = rayHit.collider.gameObject;
            HighlightObject(hitObject);
            Debug.Log("Object hit");
        }
        else
        {
            ClearHighlighted();
        }
    }

    void Update()
    {
        HighlightObjectInCenterOfCam();
    }
}