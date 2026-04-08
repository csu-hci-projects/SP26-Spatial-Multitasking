using UnityEngine;

public class ShipScanTarget : MonoBehaviour
{
    private ScannerZone[] zones;

    private void Awake()
    {
        zones = GetComponentsInChildren<ScannerZone>(true);
    }

    public int TotalZones
    {
        get
        {
            return zones != null ? zones.Length : 0;
        }
    }
}