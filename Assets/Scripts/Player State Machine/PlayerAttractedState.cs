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
        float dist = Vector2.Distance(player.Position, player.CurrentPlanet.Position);

        if (dist <= player.LandingRange)
        {
            // Land on planet
            player.SwitchState(player.DockedState);
        }
        else
        {
            if (dist > player.AttractionRange)
            {
                // Exit gravity
                player.SwitchState(player.FloatingState);
            }
        }
    }
    
    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
        // Player gravitates toward planet
        Vector2 dir = (player.CurrentPlanet.Position - player.Position).normalized;
        player.rb.velocity = new Vector2(dir.x, dir.y) * player.AttractionForce;
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
        
    }
}
