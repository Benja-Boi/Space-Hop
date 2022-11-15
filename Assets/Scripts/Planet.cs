using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Planet : MonoBehaviour
{
    private CircleCollider2D _cc;
    private Rigidbody2D _rb;

    public float Radius { get; private set; } = 1f;
    public Rigidbody2D rb { get; }

    public Vector2 Position => this.transform.position;

    private void Awake()
    {
        _cc = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        Radius = transform.localScale.x * _cc.radius;
    }

    public void CatchPlayer()
    {
        _rb.isKinematic = true;
    }
}
