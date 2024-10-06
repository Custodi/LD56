using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPawn : Pawn
{
    [SerializeField]
    private float _minimalDistanceToMouse = 1f;

    [SerializeField]
    private float _maxDistanceToMouse = 5f;

    [SerializeField]
    private float _maxPower = 5f;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private GameObject _particleSystem;

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
        if (Input.GetMouseButton(0) && distance > _minimalDistanceToMouse && distance < _maxDistanceToMouse && PointerCharge.Instance.IsActive)
        {
            _rb.velocity = (mousePos - transform.position).normalized * _maxPower * Mathf.Clamp01((distance - _minimalDistanceToMouse)*2);
            //Debug.Log($"{transform.position}||{mousePos}");
        }
    }

    public override void DestroyPawn()
    {
        StartCoroutine(PawnDestroying());
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnPawnDestroyed?.Invoke(this);
    }

    IEnumerator PawnDestroying()
    {
        _particleSystem.SetActive(true);
        yield return new WaitForSeconds(1.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_rb.velocity.x, _rb.velocity.y, 0));
    }
}
