using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PointerCharge : MonoBehaviour
{
    private static PointerCharge _instance;
    public static PointerCharge Instance { get { return _instance; } }
  
    private bool _isActive = false;

    public bool IsActive { get => _isActive; }


    [SerializeField]
    private float _maxCharge = 4f;

    [SerializeField]
    private float _chargeSpeed = 4f;

    [SerializeField]
    private float _chargePenaltySpeed = -2f;

    [SerializeField]
    private Image _chargeBar;

    [SerializeField]
    private AnimationCurve _fillVariation;

    private float _currentCharge;

    private bool _isBlocked = false;

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        _currentCharge = _maxCharge;
    }

    private void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out movePos);

        Vector3 mousePos = transform.TransformPoint(movePos);

        //Move the Object/Panel
        transform.position = mousePos;

        if (_isBlocked)
        {
            _chargeBar.enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            _chargeBar.color = new Vector4(0.8f,0,0,1);
            _isActive = false;
            _currentCharge += Time.deltaTime * (_chargeSpeed + _chargePenaltySpeed);
            if(_currentCharge >= _maxCharge)
            {
                _currentCharge = _maxCharge;
                _isBlocked = false;
                _chargeBar.color = new Vector4(1, 1, 1, 1);
            }
            _chargeBar.fillAmount = _currentCharge / _maxCharge;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                _chargeBar.enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);
                _currentCharge -= Time.deltaTime;
                _isActive = true;
                if(_currentCharge <= 0)
                {
                    _isActive = false;
                    _isBlocked = true;
                }
            }
            else
            {
                _currentCharge += Time.deltaTime * _chargeSpeed;
                _currentCharge = Mathf.Clamp(_currentCharge, 0, _maxCharge);
                if(_currentCharge >= _maxCharge)
                {
                    _chargeBar.enabled = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                _isActive = false;
            }
            _chargeBar.fillAmount = _fillVariation.Evaluate(_currentCharge / _maxCharge);
        }

        
    }
}
