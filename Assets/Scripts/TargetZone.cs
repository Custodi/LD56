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

    private int _pawnsInZone = 0;

    List<GameObject> _gameObjects = new List<GameObject>();

    private void Start()
    {
        _text.text = _pawnsInZone.ToString() + "/" + _pawnsTarget.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<T>(out _) && _gameObjects.Contains(collision.gameObject) == false)
        {
            _gameObjects.Add(collision.gameObject);
            _pawnsInZone++;
            _text.text = _pawnsInZone.ToString() + "/" + _pawnsTarget.ToString();
            if(_pawnsInZone >= _pawnsTarget)
            {
                OnTargetZoneCompleted?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<T>(out _) && _gameObjects.Contains(collision.gameObject) == true)
        {
            _gameObjects.Remove(collision.gameObject);
            _pawnsInZone--;
            _text.text = _pawnsInZone.ToString() + "/" + _pawnsTarget.ToString();
        }
    }
}
