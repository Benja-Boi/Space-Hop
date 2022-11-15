using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDockedState : PlayerBaseState
{
    private bool _isCharging = false;
    private float _releaseDelay;
 
    public override string Name => "Docked";
    
    public override void OnStateEnter(PlayerStateManager player)
    {
        player.controller.Velocity = 0f;
        var transform = player.transform;
        transform.up = player.CurrentPlanet.transform.up;
        transform.position = new Vector2(player.CurrentPlanet.Position.x, player.CurrentPlanet.Position.y + player.CurrentPlanet.Radius);
    }

    public override void OnStateExit(PlayerStateManager player)
    {
        
    }

    public override void OnStateUpdate(PlayerStateManager player)
    {
        CheckIfCharging(player);
        if (_isCharging)
        {
            PlayerRotation(player);
        }
    }
    
    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
    }

    private void PlayerRotation(PlayerStateManager player)
    {
        player.controller.Velocity = 0;
        Vector2 mousePos = player.cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 anchorPos = player.CurrentPlanet.Position;
        float distance = Vector2.Distance(mousePos, anchorPos);

        if (distance >= player.CurrentPlanet.Radius)
        {
            Vector2 dir = -(mousePos - anchorPos).normalized;
            var transform = player.transform;
            transform.position = anchorPos + dir * player.CurrentPlanet.Radius;
            transform.up = dir;
            player.controller.Direction = dir;
        }
    }

    private void CheckIfCharging(PlayerStateManager player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCharging(player);
        }

        if (Input.GetMouseButtonUp(0))
        {
            LaunchPlayer(player);
        }
    }

    private void StartCharging(PlayerStateManager player)
    {
        _isCharging = true;
    }
    
    private void LaunchPlayer(PlayerStateManager player){
        _isCharging = false;
        player.controller.Velocity = 5;
        PlanetManager.Instance.DisableGravity(.2f, player.CurrentPlanet);
        player.SwitchState(player.FloatingState);
    }


}
