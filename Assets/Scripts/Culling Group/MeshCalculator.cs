using UnityEngine;


/// <summary>
/// It calculates the center point of the given mesh 
/// and half of the longest side of its bound as radius.
/// </summary>
public static class MeshDataGetter 
{
    /// <summary>
    /// If no mesh data can be obtained as a result of the operations performed,
    /// I use the value 1 as the default.
    /// </summary>
    private const float DEFAULT_ITEM_RADIUS = 1f;

    /// <summary>
    /// It returns half of the longest edge of the mesh.
    /// </summary>
    /// <param name="pMesh"></param>
    /// <returns>Half of longest edge </returns>
    private static float GetHalfOfLongestEdge(Mesh pMesh)
    {
        Vector3 BoxSize = pMesh.bounds.size;
        float Radius = Mathf.Max(BoxSize.x,BoxSize.y,BoxSize.z);
        return Radius/2;
    }


    /// <summary>
    /// It separates the operations according to the given transform.
    /// For items with LOD Group, transactions are made with LOD group.
    /// For items with MeshFilter, transactions are made with MeshFilter.
    /// </summary>
    /// <param name="pTransform">Transforms that has MeshFilter or LOD group</param>
    /// <returns>
    /// Returns Half of longest edge for transforms has MeshFilter
    /// Returns LODGroup.size/2 for transforms has LOD Group
    /// </returns>
    public static float GetItemRadius(Transform pTransform) 
    {
            if(pTransform.TryGetComponent(out MeshFilter MF))
            {
               return GetHalfOfLongestEdge(MF.mesh);
            }
            else if(pTransform.TryGetComponent(out LODGroup LODGroup))
            {
               return LODGroup.size/2;
            }
            else
            {
                return DEFAULT_ITEM_RADIUS;
            }
    }

    /// <summary>
    /// It separates the operations according to the given transform.
    /// For items with LOD Group, transactions are made with LOD group.
    /// For items with MeshFilter, transactions are made with MeshFilter.
    /// </summary>
    /// <param name="pTransform">Transforms that has MeshFilter or LOD group</param>
    /// <returns>
    /// Returns mesh center point.
    /// if can not calculate returns transform.position
    /// </returns>
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
