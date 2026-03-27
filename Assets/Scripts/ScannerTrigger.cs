using UnityEngine;
using TMPro;

public class ScannerTrigger : MonoBehaviour
{
    public TextMeshPro manifestText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            manifestText.text = "Ship ID: A12\nCargo: Medical Supplies\nDestination: Europa Port\nStatus: Scan Complete";
        }
    }
}