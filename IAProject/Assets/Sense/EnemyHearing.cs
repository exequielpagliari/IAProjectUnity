using DesignPatterns.StateNPC;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    NPCController npc;

    public float hearingThreshold = 5f; // Distancia m�nima para reaccionar

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
        Debug.Log("Enemigo escuch� algo y va a investigar");
        // Transici�n a estado de "Suspicious"
        npc.SetAlertState(noisePosition);
    }
}