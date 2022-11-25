using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GravityGraphics : MonoBehaviour
{
    [SerializeField] private float opacity = 0.6f;
    [SerializeField] SpriteRenderer planetRenderer;
    
    private Planet _parentPlanet;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _parentPlanet = GetComponentInParent<Planet>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        float localScale = 2 * (_parentPlanet.attractionRadius / _parentPlanet.transform.localScale.x);
        transform.localScale = new Vector3(localScale, localScale, 1);
        Color parentColor = planetRenderer.color;
        _renderer.color = new Color(parentColor.r, parentColor.g, parentColor.b, opacity);
    }
}
