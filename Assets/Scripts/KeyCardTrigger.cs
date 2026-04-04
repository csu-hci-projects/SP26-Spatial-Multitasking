using UnityEngine;

public class KeyCardTrigger : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyCard"))
        {
            shipCaseManager.ActivateKeyCard();
        }
    }
}