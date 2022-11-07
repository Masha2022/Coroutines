using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;


public class RecoloringBehaviour : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private float _recoloringTime = 0.2f;
    private float _delayBeforeNextCube = 0.1f;

    [UsedImplicitly]
    public void ChangeColors()
    {
        StartCoroutine(ChangeColors(_recoloringTime, _delayBeforeNextCube));
    }

    public IEnumerator ChangeColors(float recoloringTime, float delayBeforeNextCube)
    {
        var randomColor = Random.ColorHSV();
        for (var row = 0; row < _spawnerCubes.Cubes.GetLength(0); row++)
        for (var column = 0; column < _spawnerCubes.Cubes.GetLength(1); column++)
        {
            var cube = _spawnerCubes.Cubes[row, column];
            var cubeRenderer = cube.GetComponent<Renderer>();
            StartCoroutine(ChangeColor(cubeRenderer, recoloringTime, randomColor));
            yield return new WaitForSeconds(_delayBeforeNextCube);
        }
    }

    public IEnumerator ChangeColor(Renderer cubeRenderer, float recoloringTime, Color randomColor)
    {
        var startColor = cubeRenderer.material.color;
        var currentTime = 0.0f;
        while (currentTime < recoloringTime)
        {
            var currentColor = Color.Lerp(startColor, randomColor, currentTime / recoloringTime);
            cubeRenderer.material.color = currentColor;
            currentTime += Time.deltaTime;
            yield return null;
        }

        cubeRenderer.material.color = randomColor;
    }
}