using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Detects manually if obj is being seen by the main camera

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
        //CheckObjects();
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
    async void CheckObjects()
    {
        while (true)
        {
            //planes = GeometryUtility.CalculateFrustumPlanes(cam);
            //if (GeometryUtility.TestPlanesAABB(planes, bounds))
            //{
            //    objRenderer.enabled = true;
            //}
            //else
            //{
            //    objRenderer.enabled = false;
            //}
            await Task.Delay(10);
        }
        
    }
}