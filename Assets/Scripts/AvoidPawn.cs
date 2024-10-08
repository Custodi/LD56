using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPawn : Pawn
{
    [SerializeField]
    private float _minimalDistanceFromMouse = 5f;

    [SerializeField]
    private float _maxPower = 5f;

    [SerializeField]
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);


        var distance = Vector2.Distance(transform.position, mousePos);
        if (distance < _minimalDistanceFromMouse)
        {
            _rb.velocity = (transform.position - mousePos).normalized * _maxPower * Mathf.Clamp01((_minimalDistanceFromMouse - distance) / _minimalDistanceFromMouse);
        }
    }

    Pawn pawnBuff;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Pawn>(out pawnBuff))
        {
            if(pawnBuff is not AvoidPawn)
            {
                collision.rigidbody.gravityScale = 0;
                collision.rigidbody.velocity = Vector3.zero;
                collision.rigidbody.angularVelocity = 0f;
                pawnBuff.DestroyPawn();
            }
        }
    }

    private void OnDestroy()
    {
        OnPawnDestroyed?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_rb.velocity.x, _rb.velocity.y, 0));
    }
}
