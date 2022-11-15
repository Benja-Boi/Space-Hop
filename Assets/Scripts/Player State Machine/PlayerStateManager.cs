using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
public class PlayerStateManager : MonoBehaviour
{
    //
    public float LaunchBaseForce { get; } = 50f;
    public float AttractionForce { get; } = 5f;
    public float AttractionRange { get; } = 0.3f;
    public float LandingRange { get; } = 0.1f;
    public float MaxDragDistance { get; } = 1f;
    public Planet CurrentPlanet { get; set; }
    
    // Components
    public Rigidbody2D rb;
    public SpringJoint2D sj;
    public Camera cam;
    
    
    // States
    [SerializeField] private string currentState;
    private PlayerBaseState _currentState;
    public PlayerFloatingState FloatingState { get; } = new PlayerFloatingState();
    public PlayerAttractedState AttractedState { get; } = new PlayerAttractedState();
    public PlayerDockedState DockedState { get; } = new PlayerDockedState();
    
    // Properties
    public Vector2 Position => transform.position;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        cam = FindObjectOfType<Camera>();
        CurrentPlanet = FindObjectOfType<Planet>();
        transform.position = new Vector2(CurrentPlanet.Position.x, CurrentPlanet.Position.y + CurrentPlanet.Radius);
    }

    private void Start()
    {
        SwitchState(DockedState);
    }

    private void Update()
    {
        _currentState.OnStateUpdate(this);
    }

    private void FixedUpdate()
    {
        _currentState.OnStateFixedUpdate(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        currentState = _currentState.Name;
        Debug.Log("Switched state to: " + currentState);
        _currentState.OnStateEnter(this);
    }
}
