using UnityEngine;

public class BlueBlockProximity : MonoBehaviour
{
    private Renderer blockRenderer;
    private Color originalColor;

    private void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        originalColor = blockRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        blockRenderer.material.color = Color.white;
    }

    private void OnTriggerExit(Collider other)
    {
        blockRenderer.material.color = originalColor;
    }
}