using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;


    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Instantiate(_player, transform.position, transform.rotation);
    }
}
