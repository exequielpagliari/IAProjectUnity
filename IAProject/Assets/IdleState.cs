using DesignPatterns.StateNPC;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private NPCController npc;

    [SerializeField]
    float timeStop = 2f;
    float timeLapsed;
    public IdleState(NPCController npc)
    {
        this.npc = npc;
    }
    public void Enter()
    {
        timeLapsed = Time.time + timeStop;
        npc.GetComponent<NavMeshAgent>().isStopped = true;
        // code that runs when we first enter the state
    }
    public void Update()
    {
        if(timeLapsed < Time.time) 
            npc.StateMachine.Initialize(npc.StateMachine.patrolState);
        // Here we add logic to detect if the conditions exist to 
        // transition to another state…
    }
    public void Exit()
    {
        Debug.Log("Exit Idle State");
        // code that runs when we exit the state
    }
}
