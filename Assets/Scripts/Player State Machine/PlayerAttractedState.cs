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
        Gravitate(player);
    }

    public override void OnStateFixedUpdate(PlayerStateManager player)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player)
    {
        
    }

    private void Gravitate(PlayerStateManager player)
    {
        Planet p = player.CurrentPlanet;
        float dist = Vector2.Distance(player.Position, p.Position);
        if (dist >= p.AttractionRadius)
        {
            // If player exited gravity field, Exit orbit
            player.SwitchState(player.FloatingState);
        }
        else
        {
            if (dist > p.Radius)
            {
                // Player gravitates toward planet
                Vector2 dirToPlanet = (p.Position - player.Position).normalized;
                //float adjustedGravitationModifer = p.GravitationModifier * Time.deltaTime;
                //Vector2 moveDir = adjustedGravitationModifer * dirToPlanet + (1 - adjustedGravitationModifer) * player.controller.Direction;
                //player.controller.PointTowards(moveDir);
                //player.controller.Direction = moveDir;
                player.controller.AddForce(Time.deltaTime * p.gravitationModifier * dirToPlanet);
            }
            else
            {
                // Land on planet
                player.SwitchState(player.DockedState);
            }
        }
        

    }
}
