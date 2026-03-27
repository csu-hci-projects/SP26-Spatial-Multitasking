using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class ScannerTrigger : MonoBehaviour
{
    public TextMeshPro manifestText;
    public float scanDistance = 50f;

    private Grabbable grabbable;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable != null && grabbable.SelectingPointsCount > 0)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, scanDistance))
            {
                if (hit.collider.CompareTag("Ship"))
                {
                    manifestText.text = "SCAN SUCCESSFUL\n\nShip ID: A12\nCargo Verified";
                }
            }
        }
    }
}