using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;
    private List<GameObject> _activeTiles = new List<GameObject>();
    private float _spawnPos = 0;
    private float _tileLength = 20;

    [SerializeField] private Transform _player;
    private int _startTiles = 6;

    private void Start()
    {
        SpawnTile(0);
    }

    private void Update()
    {
        if (_player.position.z > _spawnPos - (_startTiles * _tileLength))
        {
            SpawnTile(Random.Range(0, _tiles.Length));
            //DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(_tiles[tileIndex], transform.forward * _spawnPos, transform.rotation);
        _activeTiles.Add(nextTile);
        _spawnPos += _tileLength;
    }

    private void DeleteTile()
    {
        Destroy(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }
}
