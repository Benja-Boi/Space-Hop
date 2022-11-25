using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStateManager : MonoBehaviour
{

    public static event Action<Planet> PlayerDocked;

    // Components
    public Planet CurrentPlanet { get; set; }
    public PlayerController controller;
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
    }

    private void Start()
    {
        FindStartingStar();
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
        _currentState.OnStateEnter(this);
        if (_currentState == DockedState)
        {
            PlayerDocked?.Invoke(CurrentPlanet);
        }
        Debug.Log("Switched state to: " + currentState);
    }

    private void FindStartingStar()
    {
        float minDist = 10000f;
        Planet closestPlanet = null;
        foreach(Planet p in PlanetManager.Instance.Planets)
        {
            float dist = Vector2.Distance(transform.position, p.Position);
            if (dist < minDist)
            {
                minDist = dist;
                closestPlanet = p;
            }
        }

        if (closestPlanet == null)
        {
            throw new Exception("No planet found closer than 10000 units");
        }

        CurrentPlanet = closestPlanet;
        transform.position = new Vector2(CurrentPlanet.Position.x, CurrentPlanet.Position.y + CurrentPlanet.Radius);
    }
}

public enum PlayerStates
{
    Docked,
    Floating,
    Attracted
}