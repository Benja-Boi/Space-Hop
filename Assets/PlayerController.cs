using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _drag;
    [SerializeField] private float _hopForce;
    private float _velocity;
    public float Velocity { get { return _velocity; } set { _velocity = value; }}
    private Vector2 _direction;
    public Vector2 Direction { get { return _direction; } set { _direction = value; }}
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _velocity = 0;
        _direction = Vector2.up;
    }

    private void Update()
    {
        UpdatePosition();
        if (Input.GetKeyDown("space"))
        {
            _velocity = 1;
        }
    }

    private void UpdatePosition()
    {
        _transform.position += (Vector3) (Time.deltaTime * _velocity * _direction);
    }

    public void Float()
    {
        if (_velocity <= 0)
        {
            _velocity = 0;
        }
        else
        {
            _velocity -= Time.deltaTime * _drag;
        }
    }

    public void Rotate()
    {
        
    }
    
}
