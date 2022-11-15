using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void OnStateEnter(PlayerStateManager player);
    
    public abstract void OnStateExit(PlayerStateManager player);
    
    public abstract void OnStateUpdate(PlayerStateManager player);
    
    public abstract void OnStateFixedUpdate(PlayerStateManager player);
    
    public abstract void OnCollisionEnter(PlayerStateManager player);

    public abstract string Name { get; }
}