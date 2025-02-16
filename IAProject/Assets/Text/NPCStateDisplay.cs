using UnityEngine;
using TMPro;
using DesignPatterns.StateNPC;

public class NPCStateDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // Arrastra aquí el TextMeshPro desde la Inspector
    public string npcState = "Idle"; // Estado inicial
    NPCController npcController;

    private void Start()
    {
        npcController = GetComponent<NPCController>();
    }

    void Update()
    {
        npcState = npcController.StateMachine.CurrentState.ToString();
        textMesh.text = npcState; // Actualiza el texto con el estado del NPC
    }

    public void SetState(string newState)
    {
        npcState = newState;
    }
}