using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerCubes : MonoBehaviour
{
    public GameObject[,] Cubes => _cubes;
    
    [SerializeField]
    private GameObject _cubePrefab;
    private GameObject[,] _cubes = new GameObject[20, 20];
    private WaitForSeconds _waitBeforeNewSpawn;

    private void Awake()
    {
        _waitBeforeNewSpawn = new WaitForSeconds(0.04f);
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        var startPosition = _cubePrefab.transform.position;
        float offset = 0.3f;
        for (float row = 0; row < _cubes.GetLength(0); row++)
        {
            for (float column = 0; column < _cubes.GetLength(1); column++)
            {
                _cubes[(int)row, (int)column] = Instantiate(_cubePrefab,
                    new Vector3(startPosition.x + column * offset, startPosition.y + row * offset,
                        _cubePrefab.transform.position.z),
                    Quaternion.identity);
                yield return _waitBeforeNewSpawn;
            }
        }
    }
}