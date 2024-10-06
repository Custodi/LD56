using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesMover : MonoBehaviour
{
    [SerializeField]
    private Transform _eyes;

    [SerializeField]
    private Vector2 _xBoundaries;

    [SerializeField]
    private Vector2 _yBoundaries;

    [SerializeField]
    private float _timeUpdate;

    [SerializeField]
    private float _speed;

    private void Start()
    {
        StartCoroutine(EyesMoving());
    }

    IEnumerator EyesMoving()
    {
        while(true)
        {
            var newPos = new Vector2(Random.Range(_xBoundaries.x, _xBoundaries.y), Random.Range(_yBoundaries.x, _yBoundaries.y));
            _eyes.DOLocalMove(newPos, newPos.magnitude / _speed);
            yield return new WaitForSeconds(_timeUpdate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_eyes.position + Vector3.right * _xBoundaries.x, _eyes.position + Vector3.right * _xBoundaries.y);
        Gizmos.DrawLine(_eyes.position + Vector3.up * _yBoundaries.x, _eyes.position + Vector3.up * _yBoundaries.y);
    }
}
