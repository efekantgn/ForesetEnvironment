using System;
using System.Collections.Generic;
using UnityEngine;

public class CullingGroupManager : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private float[] _groupDistances;
    [SerializeField] private CullingGroup _group;
    [SerializeField] private BoundingSphere[] _spheres;
    private List<Transform> _groupItems;


    private void Awake() {
        _group = new CullingGroup();
        _groupItems = new List<Transform>();

    }

    private void Start() {

        FillGroupItems();
        _spheres = new BoundingSphere[_groupItems.Count];
        SetSphereDatas();
        InitializeCullingGroup();
    }

    private void Update() {
        _group.SetDistanceReferencePoint(_player);
    }

    private void OnDestroy() {
        _group.Dispose();
        _group=null;
    }
    private void FillGroupItems(){

        for (int i = 0; i < transform.childCount; i++)
        {
            _groupItems.Add(transform.GetChild(i));
        }

    }
    private void SetSphereDatas(){

        for(int i =0;i < _groupItems.Count;i++)
        {
            _spheres[i].position=_groupItems[i].position;
            _spheres[i].radius=1f;
        }
    }

    private void InitializeCullingGroup(){

        _group.SetBoundingSpheres(_spheres);
        _group.SetBoundingDistances(_groupDistances);
        _group.targetCamera = Camera.main;
        _group.onStateChanged += GroupStateChanged;
    }

    private void GroupStateChanged(CullingGroupEvent pEvent)
    {
        if(pEvent.hasBecomeVisible||pEvent.currentDistance<2)
            _groupItems[pEvent.index].gameObject.SetActive(true);
        else
            _groupItems[pEvent.index].gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.position,_groupDistances[0]);
        Gizmos.DrawWireSphere(_player.position,_groupDistances[1]);
        Gizmos.DrawWireSphere(_player.position,_groupDistances[2]);
    }
}
