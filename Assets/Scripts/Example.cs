using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Example : MonoBehaviour
{
    
    GameObject obj;
    Renderer objRenderer;

    Camera cam;
    Plane[] planes;
    Bounds bounds;

    void Start()
    {
        obj = gameObject;
        cam = Camera.main;

        objRenderer = GetComponent<Renderer>();
        bounds=objRenderer.bounds;
    }


    private void LateUpdate()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, bounds))
        {
            objRenderer.enabled = true;
        }
        else
        {
            objRenderer.enabled = false;
        }
    }
    
}