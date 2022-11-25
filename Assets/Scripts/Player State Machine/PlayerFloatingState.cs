using System;
using JetBrains.Annotations;
using UnityEditor.MPE;
using UnityEngine;

public class PlayerFloatingState : PlayerBaseState
{
    public override string Name => "Floating";

    public override void OnStateEnter(PlayerStateManager player)
    {
        
    }

    public override void OnStateExit(PlayerStateManager player)
    {
        
    }

    public override void OnStateUpdate(PlayerStateManager player)
    {
        player.controller.Float();
        SeekAndLand(player);
    }
    
    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
        
    }

    private void SeekAndLand(PlayerStateManager player)
    {
        Planet p = SeekPlanet(player);
        if (p != null)
        {
            player.CurrentPlanet = p;
            player.SwitchState(player.AttractedState);
        }
    }
    
    private Planet SeekPlanet(PlayerStateManager player)
    {
        // Get list of planets
        Planet[] planets = PlanetManager.Instance.Planets;
        Planet closestPlanet = null;
        float minDist = 1000f;
        
        // Find the closest planet that attracts the player
        foreach (Planet p in planets)
        {
            if (p.GravityEnabled)
            {
                float dist = Vector2.Distance(player.Position, p.Position);
                if ((dist <= p.AttractionRadius) && (dist < minDist))
                {
                    minDist = dist;
                    closestPlanet = p;
                }
            }
        }
        
        // If such planet is found, return it
        return closestPlanet;
    }

}
