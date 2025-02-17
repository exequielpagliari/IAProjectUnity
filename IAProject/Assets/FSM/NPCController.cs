using BigRookGames.Weapons;
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
        [SerializeField]
        GunfireController WeaponController;

        private StateMachine stateMachine;

        [SerializeField]
        [Range(0f, 120f)]
        float timeStopIdleTimeMax;
        [SerializeField]
        [Range(0f, 120f)]
        float timeStopIdleTimeMin;
        [SerializeField]
        [Range(0f, 120f)]
        float timeStopPatrolTimeMax;
        [SerializeField]
        [Range(0f, 120f)]
        float timeStopPatrolTimeMin;
        [SerializeField]
        [Range(0f, 120f)]
        float timeStopLazzyTimeMax;
        [SerializeField]
        [Range(0f, 120f)]
        float timeStopLazzyTimeMin;


        [SerializeField]
        [Range(0f,5f)]
        float maxShootRange = 5;

        [SerializeField]
        [Range(0f, 5f)]
        float fireRate = 3f;

        [SerializeField]
        float maxSpeedPatrol = 3.5f;

        [SerializeField]
        float maxSpeedAggressive = 5f;

        [SerializeField]
        public GameObject gunTransform;

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
            player = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponentInChildren<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            // update the current State
           stateMachine.Update();
           animator.SetFloat("Speed",navMeshAgent.velocity.magnitude);

            if (Input.GetKey(KeyCode.Space))
            {
                ShootRifle();
            }

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


        public float GetTimeStopIdleTimeMax()
        {
            return timeStopIdleTimeMax;
        }

        public float GetTimeStopIdleTimeMin()
        {
            return timeStopIdleTimeMin;
        }

        public float GetTimeStopPatrolTimeMax()
        {
            return timeStopPatrolTimeMax;
        }

        public float GetTimeStopPatrolTimeMin()
        {
            return timeStopPatrolTimeMin;
        }

        public float TimeStopLazzyTimeMax()
        {
            return timeStopLazzyTimeMax;
        }

        public float GetTimeStopLazzyTimeMin()
        {
            return timeStopLazzyTimeMin;
        }

        public void SetAlertState(Vector3 location)
        {
            navMeshAgent.destination = location;
            stateMachine.Initialize(stateMachine.alertState);
        }

        public void SetAggressiveState(Vector3 location)
        {
            navMeshAgent.destination = player.transform.position;
            stateMachine.Initialize(stateMachine.aggressiveState);
        }

        public float GetFireRate()
            { return fireRate; }

        public float GetMaxShotRange()
        { return maxShootRange; }


        public void ShootRifle()
        {
            WeaponController.FireWeapon();
        }

        public float GetMaxSpeedAggressive()
            { return maxSpeedAggressive; }

        public float GetMaxSpeedPatrol()
        {  return maxSpeedPatrol; }
    }



}