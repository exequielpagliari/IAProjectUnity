using UnityEngine;

public class FootstepNoise : MonoBehaviour
{
    public float walkNoiseRadius = 3f;
    public float runNoiseRadius = 7f;
    public float crouchNoiseRadius = 1.5f;

    private SphereCollider noiseCollider;
    private CharacterController player;

    void Start()
    {
        player = GetComponent<CharacterController>();
        noiseCollider = gameObject.AddComponent<SphereCollider>();
        noiseCollider.isTrigger = true;
    }

    void Update()
    {
        AdjustNoiseRadius();
    }

    void AdjustNoiseRadius()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // Corriendo
            noiseCollider.radius = runNoiseRadius;
        else if (Input.GetKey(KeyCode.C)) // Agachado
            noiseCollider.radius = crouchNoiseRadius;
        else // Caminando normal
            noiseCollider.radius = walkNoiseRadius;
    }
}