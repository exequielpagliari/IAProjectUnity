using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace DesignPatterns.StateNPC
{
    // simple FPS Controller (logic from FPS Starter)

    public class NPCController : MonoBehaviour
    {


        private StateMachine stateMachine;

        public StateMachine StateMachine => stateMachine;

        public Animator animator;
        public NavMeshAgent navMeshAgent;
        public GameObject[] navPoints;
        public GameObject head;
        public GameObject ligth;
        public LayerMask layerMask;
        public GameObject player;

        private void Awake()
        {

            navMeshAgent = GetComponent<NavMeshAgent>();

            // initialize state machine
            stateMachine = new StateMachine(this);
        }

        private void Start()
        {
            stateMachine.Initialize(stateMachine.idleState);
            navMeshAgent.destination = GetPointPatrol().position;

        }

        private void Update()
        {
            // update the current State
           stateMachine.Update();




        }

        private void LateUpdate()
        {
            CalculateVertical();
            Move();
        }

        private void Move()
        {

        }

        private void CalculateVertical()
        {

        }

        public Transform ObtainPlayerPosition()
        {
            return player.transform;
        }



        public Transform GetPointPatrol()
        {
            GameObject randomGameObject = navPoints[Random.Range(0, navPoints.Length)];
            return randomGameObject.transform;
        }
    }


}