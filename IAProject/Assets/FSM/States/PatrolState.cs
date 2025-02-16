using DesignPatterns.StateNPC;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private NPCController npc;


    float timeLapsed;
    public PatrolState(NPCController npc)
    {
        this.npc = npc;
    }
    public void Enter()
    {
        Debug.Log("Enter Patrol State");
        timeLapsed = Time.time + Random.Range(npc.GetTimeStopPatrolTimeMin(),npc.GetTimeStopPatrolTimeMax());
        npc.GetComponent<NavMeshAgent>().isStopped = false;
        npc.GetComponent<NavMeshAgent>().destination = npc.GetPointPatrol().position;
        npc.GetComponent<Renderer>().material.color = Color.green;
        // code that runs when we first enter the state
    }
    public void Update()
    {
        if (npc.GetComponent<NavMeshAgent>().remainingDistance < .5f)
            npc.GetComponent<NavMeshAgent>().destination = npc.GetPointPatrol().position;
        if (timeLapsed < Time.time)
            npc.StateMachine.Initialize(npc.StateMachine.idleState);
        // Here we add logic to detect if the conditions exist to 
        // transition to another state…
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
