using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class ScannerTrigger : MonoBehaviour
{
    public TextMeshPro manifestText;
    public float scanDistance = 50f;
    public ShipCaseManager shipCaseManager;

    private Grabbable grabbable;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (shipCaseManager == null)
        {
            return;
        }

        if (!shipCaseManager.CaseStarted)
        {
            return;
        }

        if (grabbable != null && grabbable.SelectingPointsCount > 0)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, scanDistance))
            {
                if (hit.collider.CompareTag("Ship"))
                {
                    shipCaseManager.ShowScanResult();
                }
            }
        }
    }
}