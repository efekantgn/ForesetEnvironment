using UnityEditor;
using UnityEngine;


/// <summary>
/// The class responsible for dynamically loading and unloading objects.
/// </summary>
public class CullingGroupManager : MonoBehaviour
{

    [SerializeField] private Transform _player;

    /// <summary>
    /// What is the maximum distance for objects will be activated?
    /// </summary>
    [SerializeField] private float _renderDistance;

    
    private CullingGroup _group;
    private BoundingSphere[] _spheres;

    /// <summary>
    /// The value we want some objects to be active when they are near me.
    /// Exmple: When we are on the bridge and do not look at it,
    /// it closes and we fall off the bridge.
    /// </summary>
    private float _mustRenderDistance=1f;
    private Transform[] _groupItems;

    /// <summary>
    /// Initializing the Arrays.
    /// </summary>
    private void Awake() 
    {
        _group = new CullingGroup();
        _groupItems = new Transform[transform.childCount];
        _spheres = new BoundingSphere[transform.childCount];
    }


    private void Start() 
    {
        FillGroupItems();
        SetSphereDatas();
        InitializeCullingGroup();
    }

    private void Update() 
    {
        _group.SetDistanceReferencePoint(_player);
    }

    private void OnDestroy() 
    {
        _group.Dispose();
        _group=null;
    }

    /// <summary>
    /// I set up a scenario in which all children of the transform to 
    /// which the script is connected would be included in the dynamic loading.
    /// </summary>
    private void FillGroupItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _groupItems[i]=transform.GetChild(i);
        }

    }

    /// <summary>
    /// Assigning the Bounding Sphere values ​​required for the Culling Group operation.
    /// I process the mesh data from the static class I created 
    /// and get the value required for the center of the mesh and radius assignment.
    /// </summary> 
    private void SetSphereDatas()
    {
        for(int i =0;i < _groupItems.Length;i++)
        {
            _spheres[i].position= MeshDataGetter.GetMeshCenter(_groupItems[i]);
            _spheres[i].radius= MeshDataGetter.GetItemRadius(_groupItems[i]);
        }
    }

    /// <summary>
    /// Initializing Culling Group values.
    /// GroupStateChanged subscribed to OnStateChanged delegate.
    /// </summary>
    private void InitializeCullingGroup()
    {
        _group.SetBoundingSpheres(_spheres);
        _group.SetBoundingDistances(new float[]{0,_mustRenderDistance,_renderDistance});
        _group.targetCamera = Camera.main;
        _group.onStateChanged += GroupStateChanged;
    }


    /// <summary>
    /// It works when onStateChanged is Invoked.
    /// If the object is visible or its distance is at the 1st zone, the gameobject is opened from the event.Index.
    /// If the object becomes invisible or its distance is in the 3rd zone, the gameobject closes.
    /// I performed the operation with GameObject because it was specified as Object in the document.
    /// My own idea would be to just turn off the Mesh and its animations.
    /// </summary>
    /// <param name="pEvent"></param>
    private void GroupStateChanged(CullingGroupEvent pEvent)
    {
        if(pEvent.hasBecomeVisible||pEvent.currentDistance==1)
            _groupItems[pEvent.index].gameObject.SetActive(true);
        else if(pEvent.hasBecomeInvisible||pEvent.currentDistance==3)
            _groupItems[pEvent.index].gameObject.SetActive(false);
    }



    /// <summary>
    /// It shows the BoundingSphere data of each object.
    /// </summary>
    private void OnDrawGizmosSelected() 
    {
        if(!EditorApplication.isPlaying) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _spheres.Length; i++)
        {
            Gizmos.DrawWireSphere(_spheres[i].position,_spheres[i].radius);
        }
    }
}
