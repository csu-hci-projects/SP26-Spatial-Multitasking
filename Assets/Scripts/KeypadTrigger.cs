using UnityEngine;

public class KeypadTrigger : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;

    private void OnTriggerEnter(Collider other)
    {
        if (shipCaseManager == null)
        {
            return;
        }

        shipCaseManager.ShowManifest();
    }
}