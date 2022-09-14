using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float _force;

    [SerializeField]
    private float _magnetizeRange = 0.2f;
    [SerializeField]
    private Planet _currentPlanet;
    private bool _grounded;
    private Vector2 _mousePos;
    private Camera _cam;
    private Rigidbody2D _rb;
    private Collider2D _cc;

    public Vector2 Position {  get { return transform.position; } }


    private void Awake()
    {
        _cam = FindObjectOfType<Camera>();
        _rb = GetComponent<Rigidbody2D>();
        _cc = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _grounded = false;
    }

    private void Update()
    {
        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (!_grounded)
        {
            SeekPlanet();
        }
        else
        {
            RotateAroundPlanet();
        }
    }

    private void SeekPlanet()
    {
        Planet[] planets = PlanetManager.Instance.Planets;
        foreach (Planet p in planets)
        {
            float dist = Vector2.Distance(transform.position, p.Position);
            if (dist - p.Radius <= p.GravityRange)
            {
                MagnetizeToPlanet(p);
            }
        }
    }

    private void MagnetizeToPlanet(Planet i_p)
    {
        _rb.isKinematic = true;
        i_p.CatchPlayer();
        transform.up = i_p.transform.up;
        transform.position = new Vector2(i_p.Position.x, i_p.Position.y + i_p.Radius);
        transform.parent = i_p.transform;
    }

    private void RotateAroundPlanet()
    {

    }

    public void LaunchPlayer(Vector2 i_shootDir, float i_chargeAmount)
    {
        float force = _force * i_chargeAmount;
        _rb.AddForce(i_shootDir * force);

    }
}
