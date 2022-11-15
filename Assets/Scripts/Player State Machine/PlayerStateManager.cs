using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStateManager : MonoBehaviour
{
    //
    public Planet CurrentPlanet { get; set; }
    
    // Components
    [FormerlySerializedAs("Controller")] public PlayerController controller;
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
        cam = FindObjectOfType<Camera>();
        controller = GetComponent<PlayerController>();
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
