using UnityEngine;

public class PurpleBlockDisappear : MonoBehaviour
{
    private Renderer[] renderers;
    private Collider[] colliders;
    private BoxCollider triggerCollider;

    private void Start()
    {
        renderers = GetComponents<Renderer>();
        colliders = GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            if (col.isTrigger)
            {
                triggerCollider = col as BoxCollider;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        foreach (Collider c in colliders)
        {
            if (c != triggerCollider)
            {
                c.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }

        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }
    }
}