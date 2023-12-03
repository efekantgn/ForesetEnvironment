using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CullingGroupManager : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private float _renderDistance;
    [SerializeField] private float _mustRenderDistance=1f;
    [SerializeField] private CullingGroup _group;
    [SerializeField] private BoundingSphere[] _spheres;
    private Transform[] _groupItems;
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

    
    private void FillGroupItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _groupItems[i]=transform.GetChild(i);
        }

    }
    private void SetSphereDatas()
    {
        for(int i =0;i < _groupItems.Length;i++)
        {
            _spheres[i].position= MeshDataGetter.GetMeshCenter(_groupItems[i]);
            _spheres[i].radius= MeshDataGetter.GetItemRadius(_groupItems[i]);
        }
    }

    private void InitializeCullingGroup()
    {
        _group.SetBoundingSpheres(_spheres);
        _group.SetBoundingDistances(new float[]{0,_mustRenderDistance,_renderDistance});
        _group.targetCamera = Camera.main;
        _group.onStateChanged += GroupStateChanged;
    }

    private void GroupStateChanged(CullingGroupEvent pEvent)
    {
        if(pEvent.hasBecomeVisible||pEvent.currentDistance==1)
            _groupItems[pEvent.index].gameObject.SetActive(true);
        else if(pEvent.hasBecomeInvisible||pEvent.currentDistance==3)
            _groupItems[pEvent.index].gameObject.SetActive(false);
    }


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
