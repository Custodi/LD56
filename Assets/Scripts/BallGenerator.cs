using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _generatedObject;

    [SerializeField]
    private float _generationTick;

    [SerializeField]
    private Vector2Int _generationCount;

    private void Start()
    {
        StartCoroutine(BallGenerating());
    }

    IEnumerator BallGenerating()
    {
        while(true)
        {
            for (int i = 0; i < Random.Range(_generationCount.x, _generationCount.y); i++) Instantiate(_generatedObject, transform);
            yield return new WaitForSeconds(_generationTick);
        }
    }
}
