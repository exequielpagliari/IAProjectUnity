using DesignPatterns.StateNPC;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public IState CurrentState { get; private set; }
    public PatrolState patrolState;
    public IdleState idleState;

    // pass in necessary parameters into constructor 
    public StateMachine(NPCController npc)
    {
        // create an instance for each state and pass in PlayerController
        this.patrolState = new PatrolState(npc);
        this.idleState = new IdleState(npc);

    }
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}



