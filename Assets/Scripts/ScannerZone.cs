using UnityEngine;

public class ScannerZone : MonoBehaviour
{
    public ShipScanTarget ship;

    private void Awake()
    {
        if (ship == null)
        {
            ship = GetComponentInParent<ShipScanTarget>();
        }
    }
}