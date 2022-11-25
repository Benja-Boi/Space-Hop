using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Planet : MonoBehaviour
{
    private CircleCollider2D _cc;
    private Rigidbody2D _rb;

    public float radius = 1f;
    public float attractionRadius = 2f;
    public float gravitationModifier = .1f;

    
    [SerializeField] public float Radius { get => radius; private set => radius = value;}
    [SerializeField] public float AttractionRadius { get => attractionRadius; private set => attractionRadius = value;}
    [SerializeField] public float GravitationModifier { get => gravitationModifier; private set => gravitationModifier = value;}
    public bool GravityEnabled { get; set; } = true;
    public Rigidbody2D rb { get; }

    public Vector2 Position => this.transform.position;

    private void Awake()
    {
        _cc = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        //Radius = transform.localScale.x * _cc.radius;
    }

    public void CatchPlayer()
    {
        _rb.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Position, Radius);
        Gizmos.DrawWireSphere(Position, AttractionRadius);
    }
}
