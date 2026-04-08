using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class ScannerTrigger : MonoBehaviour
{
    public TextMeshPro manifestText;
    public float scanDistance = 50f;
    public ShipCaseManager shipCaseManager;

    [Header("Scan Settings")]
    public int requiredZoneCount = 3; // out of 4 zones, this means 75%

    private Grabbable grabbable;
    private ShipScanTarget currentShip;
    private HashSet<ScannerZone> scannedZones = new HashSet<ScannerZone>();
    private bool scanComplete = false;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable == null || scanComplete)
        {
            return;
        }

        if (grabbable.SelectingPointsCount <= 0)
        {
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, scanDistance))
        {
            ScannerZone zone = hit.collider.GetComponent<ScannerZone>();

            if (zone == null || zone.ship == null)
            {
                return;
            }

            if (currentShip == null)
            {
                currentShip = zone.ship;
                scannedZones.Clear();
            }

            if (zone.ship != currentShip)
            {
                return;
            }

            scannedZones.Add(zone);

            int totalZones = currentShip.TotalZones;
            int scannedCount = scannedZones.Count;

            if (manifestText != null)
            {
                manifestText.text = "Scanning ship: " + scannedCount + " / " + requiredZoneCount;
            }

            if (scannedCount >= requiredZoneCount)
            {
                scanComplete = true;

                if (manifestText != null)
                {
                    manifestText.text = "Scan complete.";
                }

                if (shipCaseManager != null)
                {
                    shipCaseManager.ShowScanResult();
                }
            }
        }
    }

    public void ResetScanner()
    {
        currentShip = null;
        scannedZones.Clear();
        scanComplete = false;

        if (manifestText != null)
        {
            manifestText.text = "Scanner ready.";
        }
    }
}