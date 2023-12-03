using UnityEngine;


/// <summary>
/// Verilen mesh üzerinden meshin center noktasını ve
/// verilen meshin boundunun en uzun kenarının yarısını yarıçap olarak hesaplıyor
/// </summary>
public static class MeshDataGetter 
{
    /// <summary>
    /// Eğer yapılan işlemler sonucunda bir mesh datasına ulaşılamazsa 
    /// Default olarak 1 değerini kullanıyorum.
    /// </summary>
    private const float DEFAULT_ITEM_RADIUS = 1f;

    /// <summary>
    /// Meshin en uzun kenarının yarısını döndürüyor.
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
    /// Verilen transforma göre işlemleri ayırıyor.
    /// LOD Group olan itemlar için LOD group üzerinden işlem yapıyor.
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
    /// Verilen transforma göre işlemleri ayırıyor.
    /// LOD Group olan itemlar için LOD group üzerinden işlem yapıyor.
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
