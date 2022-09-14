using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class InputManager : MonoBehaviour
{
    private Camera _mainCam;
    private PlayerMovement _player;
    private Rigidbody2D _rb;
    private Vector2 _mousePos;
    private Vector2 _clickPos;
    private bool _chargingShot;
    [SerializeField]
    private Vector2 _shootDir;
    [SerializeField]
    private float _chargeAmount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCam = FindObjectOfType<Camera>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        _chargingShot = false;
    }

    void Update()
    {
        if (_chargingShot)
        {
            _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 distVector = _mousePos - _clickPos;
            _shootDir = -(distVector).normalized;
            _chargeAmount = Mathf.Atan(distVector.magnitude) / (Mathf.PI / 2);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
        _chargingShot = true;
        _clickPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUp");
        _chargingShot = false;
        ShootSelf();
    }

    private void ShootSelf()
    {
        Debug.Log("Shoot");
        _player.LaunchPlayer(_shootDir, _chargeAmount);
    }
}
