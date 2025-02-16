using DesignPatterns.StateNPC;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    NPCController npc;

    public float hearingThreshold = 5f; // Distancia mínima para reaccionar

    private void Start()
    {
        npc = GetComponent<NPCController>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float noiseLevel = other.GetComponent<SphereCollider>().radius;

            if (noiseLevel >= hearingThreshold)
            {
                InvestigateNoise(other.transform.position);
            }
        }
    }

    void InvestigateNoise(Vector3 noisePosition)
    {
        Debug.Log("Enemigo escuchó algo y va a investigar");
        // Transición a estado de "Suspicious"
        npc.SetAlertState(noisePosition);
    }
}