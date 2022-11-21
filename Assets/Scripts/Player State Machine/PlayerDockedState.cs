using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDockedState : PlayerBaseState
{
    private bool _isCharging = false;
    private float _chargeAmount = 0f;
 
    public override string Name => "Docked";
    
    public override void OnStateEnter(PlayerStateManager player)
    {
        // Stop player movement and place player on top of current planet
        
        player.controller.Halt();
        player.controller.Direction = player.CurrentPlanet.transform.up;
        player.controller.PointTowards(player.CurrentPlanet.transform.up);
        player.controller.MoveToPosition(new Vector2(player.CurrentPlanet.Position.x, player.CurrentPlanet.Position.y + player.CurrentPlanet.Radius));
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
        // Handle the rotation mechanism relative to the mouse position and the current planet
        player.controller.Halt();
        Vector2 mousePos = player.cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 anchorPos = player.CurrentPlanet.Position;
        float distance = Vector2.Distance(mousePos, anchorPos);
        
        // If the mouse is inside the planet, don't apply rotation mechanism
        if (distance >= player.CurrentPlanet.Radius)
        {
            _chargeAmount = Clamp(distance - player.CurrentPlanet.Radius, 0, 1);
            Vector2 dir = -(mousePos - anchorPos).normalized;
            player.controller.MoveToPosition(anchorPos + dir * player.CurrentPlanet.Radius); ;
            player.controller.PointTowards(dir);
            player.controller.Direction = dir;
            DrawTrajectory(player.Position, dir);
        }
    }

    private void OnDrawGizmos()
    {
        
    }

    private void DrawTrajectory(Vector2 position, Vector2 dir)
    {
        Debug.DrawLine(position, position + 10 * dir);
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
        player.controller.Launch(_chargeAmount);
        PlanetManager.Instance.DisableGravity(.2f, player.CurrentPlanet);
        player.SwitchState(player.FloatingState);
    }

    private float Clamp(float x, float min, float max)
    {
        // Clamp x to a value such that min<=x<=max
        if (x < min) return min;
        if (x > max) return max;
        return x;
    }


}
