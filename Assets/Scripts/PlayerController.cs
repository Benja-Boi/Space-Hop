using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float drag;
    [SerializeField] private float hopForce;
    [SerializeField] private float velocity;
    private Vector2 _direction;
    public Vector2 Direction
    {
        get => _direction;
        set
        {
            _direction = value.normalized;
            PointTowardsMovement();
        }
    }

    private float Velocity
    {
        get => velocity;
        set => velocity = value;
    }
    private Transform _transform;
    private Collider2D _collider;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        velocity = 0;
        Direction = Vector2.up;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        _transform.position += (Vector3) (Time.deltaTime * velocity * Direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 currentPos = transform.position;
            if (Direction.x >= 0)
            {
                if (Direction.y >= 0)
                {
                    // Moving up and right, can't collide with left and bottom walls
                    if (other.bounds.min.y > currentPos.y)
                    {
                        // Upper wall
                        FlipDirY();
                    }
                    else
                    {
                        // Right wall
                        FlipDirX();
                    }
                }
                else
                {
                    // Moving down and right, can't collide with left and upper walls
                    if (other.bounds.max.y < currentPos.y)
                    {
                        // Bottom wall
                        FlipDirY();
                    }
                    else
                    {
                        // Right wall
                        FlipDirX();
                    }
                }
            }
            else{
                if (Direction.y >= 0)
                {
                    // Moving up and left, can't collide with right and bottom walls
                    if (other.bounds.min.y > currentPos.y)
                    {
                        // Upper wall
                        FlipDirY();
                    }
                    else
                    {
                        // Left wall
                        FlipDirX();
                    }
                    
                }
                else
                {
                    // Moving down and left, can't collide with right and upper walls
                    if (other.bounds.max.y < currentPos.y)
                    {
                        // Bottom wall
                        FlipDirY();
                    }
                    else
                    {
                        // Left wall
                        FlipDirX();
                    }
                }
            }
        }
    }

    private void PointTowardsMovement()
    {
        transform.up = Direction;
    }
    
    private void FlipDirX()
    {
        Direction = new Vector2(-Direction.x, Direction.y);
    }

    private void FlipDirY()
    {
        Direction = new Vector2(Direction.x, -Direction.y);
    }
    
    public void Launch(float charge)
    {
        velocity = charge * hopForce;
    }

    public void PointTowards(Vector2 direction)
    {
        transform.up = direction.normalized;
    }

    public void MoveToPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void Float()
    {
        if (velocity <= 0)
        {
            velocity = 0;
        }
        else
        {
            velocity -= Time.deltaTime * drag;
        }
    }

    public void Halt()
    {
        velocity = 0;
    }

    public void AddForce(Vector2 force)
    {
        Vector2 newMovementVector = velocity * Direction + force;
        Direction = newMovementVector.normalized;
        velocity = newMovementVector.magnitude;
    }
}
