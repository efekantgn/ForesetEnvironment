using UnityEngine;

public static class MeshDataGetter 
{
    private const float DEFAULT_ITEM_RADIUS = 1f;
    private static float GetRadiusOfMesh(Mesh pMesh)
    {
        Vector3 BoxSize = pMesh.bounds.size;
        float Radius = Mathf.Max(BoxSize.x,BoxSize.y,BoxSize.z);
        return Radius;
    }

    public static float GetItemRadius(Transform pItem) 
    {
            if(pItem.TryGetComponent(out MeshFilter MF))
            {
               return GetRadiusOfMesh(MF.mesh);
            }
            else if(pItem.TryGetComponent(out LODGroup LODGroup))
            {
               return LODGroup.size/2;
            }
            else
            {
                return DEFAULT_ITEM_RADIUS;
            }
    }
    public static Vector3 GetMeshCenter(Transform pItem)
    {

        if(pItem.TryGetComponent(out Renderer Renderer))
        {
            return Renderer.bounds.center;
        }
        else if(pItem.TryGetComponent(out LODGroup LODGroup))
        {
            return LODGroup.GetLODs()[0].renderers[0].bounds.center;
        }
        else
        {
            return pItem.position;
        }
        
    }
}
