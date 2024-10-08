using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwayPawn : Pawn
{
    [SerializeField]
    private float _maxPower = 5f;

    [SerializeField]
    private float _maxDistance = 0.25f;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private ParticleSystem _particleSystem;

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

        //var distance = Vector2.Distance(transform.position, mousePos);
        //Debug.Log($"{(transform.position - mousePos).magnitude}");
        if (Input.GetMouseButtonDown(0) && (transform.position - mousePos).magnitude <= _maxDistance)
        {
            _rb.velocity = (transform.position - mousePos).normalized * _maxPower;
        }
    }

    public override void DestroyPawn()
    {
        StartCoroutine(PawnDestroying());
    }

    IEnumerator PawnDestroying()
    {
        _particleSystem.GetComponent<ParticleSystem>().Play();
        transform.DOScale(0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        Destroy(transform.gameObject);
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
