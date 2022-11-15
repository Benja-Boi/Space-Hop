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
        player.rb.velocity = Vector2.zero;
        var transform = player.transform;
        transform.up = player.CurrentPlanet.transform.up;
        transform.position = new Vector2(player.CurrentPlanet.Position.x, player.CurrentPlanet.Position.y + player.CurrentPlanet.Radius);
        _releaseDelay = 1 / (4 * player.sj.frequency);
        int j = 0;
    }

    public override void OnStateExit(PlayerStateManager player)
    {
        
    }

    public override void OnStateUpdate(PlayerStateManager player)
    {
        CheckIfCharging(player);
        if (_isCharging)
        {
            DragPlayer(player);
        }
    }
    
    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
    }

    private void DragPlayer(PlayerStateManager player)
    {
        player.rb.velocity = Vector2.zero;
        Vector2 mousePos = player.cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 anchorPos = player.CurrentPlanet.Position;
        float distance = Vector2.Distance(mousePos, anchorPos);

        if (distance > player.MaxDragDistance)
        {
            Vector2 dir = (mousePos - anchorPos).normalized;
            player.rb.position = anchorPos + dir * player.MaxDragDistance;
        }
        else
        {
            player.rb.position = mousePos;
        }
    }

    private void CheckIfCharging(PlayerStateManager player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableSlingshot(player);
        }

        if (Input.GetMouseButtonUp(0))
        {
            LaunchPlayer(player);
        }
    }

    private void EnableSlingshot(PlayerStateManager player)
    {
        _isCharging = true;
        player.sj.connectedBody = player.CurrentPlanet.rb;
        player.sj.connectedAnchor = player.CurrentPlanet.Position;
        player.sj.enabled = true;
        player.rb.isKinematic = true;
    }
    
    private void LaunchPlayer(PlayerStateManager player){
        _isCharging = false;
        player.rb.isKinematic = false;
        player.StartCoroutine(Release(player));
    }

    private IEnumerator Release(PlayerStateManager player)
    {
        yield return new WaitForSeconds(_releaseDelay);
        player.sj.enabled = false;
        player.SwitchState(player.FloatingState);
    }

}
