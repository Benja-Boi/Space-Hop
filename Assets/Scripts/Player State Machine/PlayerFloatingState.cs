using System;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerFloatingState : PlayerBaseState
{
    public override string Name => "Floating";
    private bool _attractionEnable = false;
    private float _timer = 1f;
    
    public override void OnStateEnter(PlayerStateManager player)
    {
        _timer = 1f;
        _attractionEnable = false;
    }

    public override void OnStateExit(PlayerStateManager player)
    {
        
    }

    public override void OnStateUpdate(PlayerStateManager player)
    {
        if (_timer > 0f)
        {
            TimerCountdown();
        }
        if (_attractionEnable)
        {
            SeekPlanet(player);
        }
    }
    
    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
        
    }

    private void SeekPlanet(PlayerStateManager player)
    {
        Planet[] planets = PlanetManager.Instance.Planets;
        Planet closestPlanet = null;
        float minDist = 1000f;
        
        // Find the closest planet within attraction range
        foreach (Planet p in planets)
        {
            float dist = Vector2.Distance(player.Position, p.Position);
            if ((dist - p.Radius <= player.AttractionRange) && (dist < minDist))
            {
                minDist = dist;
                closestPlanet = p;
            }
        }
        
        // If such planet is found, move towards it
        if (closestPlanet == null) return;
        player.CurrentPlanet = closestPlanet;
        player.SwitchState(player.AttractedState);

    }

    private void TimerCountdown()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _attractionEnable = true;
        }
    }
}
