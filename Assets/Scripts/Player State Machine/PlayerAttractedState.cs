using UnityEngine;

public class PlayerAttractedState : PlayerBaseState
{
    public override string Name => "Attracted";

    public override void OnStateEnter(PlayerStateManager player)
    {
        
    }

    public override void OnStateExit(PlayerStateManager player)
    {
        
    }

    public override void OnStateUpdate(PlayerStateManager player)
    {
        Planet p = player.CurrentPlanet;
        float dist = Vector2.Distance(player.Position, p.Position);

        if (dist <= p.Radius)
        {
            // Land on planet
            player.SwitchState(player.DockedState);
        }
        else
        {
            if (dist > p.AttractionRadius)
            {
                // Exit gravity
                player.SwitchState(player.FloatingState);
            }
        }
        
        // Player gravitates toward planet
        Vector2 dirToPlanet = (p.Position - player.Position).normalized;
        player.controller.Direction =
            p.GravitationModifier * dirToPlanet + (1 - p.GravitationModifier) * player.controller.Direction;
    }

    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
        
    }
}
