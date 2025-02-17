using DesignPatterns.StateNPC;
using UnityEngine;
using UnityEngine.AI;

public class AggressiveState : IState
{
    private NPCController npc;

    [SerializeField]
    float timeStop = 10f;
    float timeLapsed;

    float timeNextShot;




    public AggressiveState(NPCController npc)
    {
        this.npc = npc;
    }
    public void Enter()
    {
        timeNextShot = Time.time + npc.GetFireRate();
        timeLapsed = Time.time + timeStop;
        npc.GetComponent<Renderer>().material.color = Color.red;
        npc.GetComponent<NavMeshAgent>().isStopped = false;
        // code that runs when we first enter the state
    }
    public void Update()
    {
        npc.GetComponent<NavMeshAgent>().destination = npc.ObtainPlayerPosition().position;
        
        if((npc.ObtainPlayerPosition().transform.position - npc.transform.position).magnitude <= 2f)
        { 
            ShootAtPlayer();
            npc.GetComponent<NavMeshAgent>().isStopped = true;

        }

        if ((npc.ObtainPlayerPosition().transform.position - npc.transform.position).magnitude > 4f)
        {
            npc.animator.SetBool("Aim", false);
            npc.navMeshAgent.isStopped = false;
        }



        if (timeLapsed < Time.time)
            npc.SetAlertState(npc.ObtainPlayerPosition().transform.position);
        // Here we add logic to detect if the conditions exist to 
        // transition to another state…
    }
    public void Exit()
    {
        Debug.Log("Exit Idle State");
        npc.animator.SetBool("Aim", false);

        // code that runs when we exit the state
    }



    public  void ShootAtPlayer()
    { 
       if(Time.time > timeNextShot)
        {
            
            npc.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            npc.animator.SetBool("Aim", true);
            Debug.LogWarning("PosibleShot");
            Shoot();
            timeNextShot = Time.time + npc.GetFireRate();
        }
    }


    private void Shoot()
    {
        Vector3 shootDirection = (npc.ObtainPlayerPosition().transform.position - npc.gunTransform.transform.position).normalized;
        RaycastHit hit;
        Debug.LogWarning("Shot");
        if (Physics.Raycast(npc.gunTransform.transform.position, shootDirection, out hit, npc.GetMaxShotRange()))
        {
            if (hit.collider.CompareTag("Player"))
            {
                //player.TakeDamage(damageAmount);
                npc.animator.SetTrigger("Shot");
                Debug.LogWarning("HitPlayer");
                hit.collider.gameObject.GetComponent<CharacterController>().Move(Vector3.zero);
            }
        }
        npc.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void RestartTimeLapsed()
    { timeLapsed = Time.time + timeStop; }
}
