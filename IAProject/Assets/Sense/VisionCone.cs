using UnityEngine;
using DesignPatterns.StateNPC;

public class VisionCone : MonoBehaviour
{
    [SerializeField][Range(-90f, 90f)] private float visionAngle = 45f;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float frequencyDetect = 2f;
    [SerializeField] private GameObject eyes; // Altura desde donde lanza el Raycast


    private GameObject player;
    private NPCController npc;
    private float nextCheckTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        npc = GetComponent<NPCController>();
        nextCheckTime = Time.time + (1f / frequencyDetect);
    }

    void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            CheckForPlayer();
            nextCheckTime = Time.time + (1f / frequencyDetect);
        }
    }

    void CheckForPlayer()
    {
        if (player == null) return;
        Vector3 direction = npc.ObtainPlayerPosition().transform.position - transform.position;
        float distanceSqr = direction.sqrMagnitude; // Optimizaci�n: usa cuadrado de la distancia

        if (distanceSqr > visionRange * visionRange) return; // Si est� fuera del rango, salir

        float angle = Vector3.Angle(transform.forward, direction);
        if (angle > visionAngle / 2) return; // Si est� fuera del cono de visi�n, salir


        Vector3 rayOrigin = eyes.transform.position; // Posici�n de los ojos


        RaycastHit hit;

        // Debug para ver la l�nea en la Scene View
        Debug.DrawRay(rayOrigin, direction * visionRange, Color.red, 0.1f);

        if (Physics.Raycast(rayOrigin, direction, out hit, visionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player Detected");
                if(npc.StateMachine.CurrentState != npc.StateMachine.aggressiveState)
                    npc.SetAggressiveState(npc.ObtainPlayerPosition().transform.position);
                else
                    npc.StateMachine.aggressiveState.RestartTimeLapsed();
            }
            else
            {
                Debug.Log("Obst�culo bloqueando la vista: " + hit.collider.name);
            }
        }

    }


    // Dibujar el cono de visi�n en la Scene View
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = eyes.transform.position;

        // Dibujar l�neas del cono
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange;

        Gizmos.DrawLine(origin, origin + leftBoundary);
        Gizmos.DrawLine(origin, origin + rightBoundary);

        // Dibujar el arco del cono
        int segments = 20;
        float angleStep = visionAngle / segments;
        Vector3 prevPoint = origin + leftBoundary;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -visionAngle / 2 + angleStep * i;
            Vector3 newPoint = origin + Quaternion.Euler(0, currentAngle, 0) * transform.forward * visionRange;
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }

        // Dibujar una l�nea al �ltimo punto del arco
        Gizmos.DrawLine(prevPoint, origin + rightBoundary);
    }
}