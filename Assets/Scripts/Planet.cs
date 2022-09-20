using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Planet : MonoBehaviour
{
    private CircleCollider2D _cc;
    private Rigidbody2D _rb;

    [SerializeField]
    private float _radius;
    public float Radius { get { return _radius; } }

    [SerializeField]
    private float _gravityRange;
    public float GravityRange { get { return _gravityRange; } }
    public Vector2 Position { get { return this.transform.position; } }

    private void Awake()
    {
        _cc = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _radius = transform.localScale.x * _cc.radius;
    }

    public void CatchPlayer()
    {
        _rb.isKinematic = true;
    }
}
