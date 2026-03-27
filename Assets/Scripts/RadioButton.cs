using UnityEngine;

public class RadioButton : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;

    private void OnTriggerEnter(Collider other)
    {
        shipCaseManager.ActivateRadioCall();
    }
}