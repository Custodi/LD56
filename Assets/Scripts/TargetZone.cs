using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetZone<T> : MonoBehaviour
{
    [SerializeField]
    private int _pawnsTarget;

    [SerializeField]
    private TextMeshPro _text;

    public Action OnTargetZoneCompleted;

    public Action OnTargetZoneUncompleted;

    private int _pawnsInZone = 0;

    List<GameObject> _gameObjects = new List<GameObject>();

    Sequence scaleSequence;

    Vector2 _initialScale;

    private bool _isZoneDone = false;

    float _upScale = 0.08f;

    private void Start()
    {
        _text.text = _pawnsInZone.ToString() + "/" + _pawnsTarget.ToString();

        _initialScale = _text.transform.localScale;

        scaleSequence = DOTween.Sequence()
           .Append(_text.transform.DOScale(_initialScale + Vector2.one * _upScale, 0.12f))
           .Append(_text.transform.DOScale(_initialScale, 0.12f)).Pause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody.gameObject.TryGetComponent<T>(out _) && _gameObjects.Contains(collision.attachedRigidbody.gameObject) == false)
        {
            _gameObjects.Add(collision.attachedRigidbody.gameObject);
            _pawnsInZone++;
            _text.text = _pawnsInZone.ToString() + "/" + _pawnsTarget.ToString();
            if (scaleSequence.IsPlaying()) scaleSequence.Restart();
            else scaleSequence.Play().OnComplete(() => scaleSequence.Rewind());
           
            if (_pawnsInZone >= _pawnsTarget && _isZoneDone == false)
            {
                _isZoneDone = true;
                OnTargetZoneCompleted?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.TryGetComponent<T>(out _) && _gameObjects.Contains(collision.attachedRigidbody.gameObject) == true)
        {
            _gameObjects.Remove(collision.attachedRigidbody.gameObject);
            _pawnsInZone--;
            if (_pawnsInZone < _pawnsTarget && _isZoneDone)
            {
                _isZoneDone = false;
                OnTargetZoneUncompleted?.Invoke();
            }

            _text.text = _pawnsInZone.ToString() + "/" + _pawnsTarget.ToString();
        }
    }
}
