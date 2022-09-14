using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DragToShoot : MonoBehaviour
{
    public Camera _mainCam;
    public float _force;

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
            Vector2 distVector = _mousePos - (Vector2)this.transform.position;
            _shootDir = -(distVector).normalized;
            _chargeAmount = Mathf.Atan(distVector.magnitude) / (Mathf.PI/2);
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
        float force = _force * _chargeAmount;
        _rb.AddForce(_shootDir * force);
    }
}
