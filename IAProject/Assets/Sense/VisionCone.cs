using UnityEngine;
using DesignPatterns.StateNPC;

public class VisionCone : MonoBehaviour
{
    [SerializeField][Range(0f, 90f)] private float visionAngle = 45f;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float frequencyDetect = 2f;
    [SerializeField] private float eyeHeight = 1.6f; // Altura desde donde lanza el Raycast

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

        Vector3 direction = player.transform.position - transform.position;
        float distanceSqr = direction.sqrMagnitude; // Optimización: usa cuadrado de la distancia

        if (distanceSqr > visionRange * visionRange) return; // Si está fuera del rango, salir

        float angle = Vector3.Angle(transform.forward, direction);
        if (angle > visionAngle / 2) return; // Si está fuera del cono de visión, salir

        Vector3 rayOrigin = transform.position + Vector3.up * eyeHeight; // Raycast desde la "cabeza"
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, direction.normalized, out hit, visionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player Detected");
                npc.SetAlertState(hit.point);
            }
        }
    }
}