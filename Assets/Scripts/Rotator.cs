using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _speedRotation;

    float timeCounter = 0;

    RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime * _speedRotation;

       _rectTransform.rotation = Quaternion.EulerAngles(0f, 0f, timeCounter);
    }
}
