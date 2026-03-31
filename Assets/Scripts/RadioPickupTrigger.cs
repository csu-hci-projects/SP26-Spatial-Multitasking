using UnityEngine;
using Oculus.Interaction;

public class RadioPickupTrigger : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;
    public Transform mouthPoint;
    public float triggerDistance = 0.18f;

    private Grabbable grabbable;
    private bool hasActivatedThisGrab = false;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable == null || shipCaseManager == null || mouthPoint == null)
        {
            return;
        }

        bool isGrabbedNow = grabbable.SelectingPointsCount > 0;

        if (!isGrabbedNow)
        {
            hasActivatedThisGrab = false;
            return;
        }

        float distanceToMouth = Vector3.Distance(transform.position, mouthPoint.position);

        if (distanceToMouth <= triggerDistance && !hasActivatedThisGrab)
        {
            shipCaseManager.ActivateRadioCall();
            hasActivatedThisGrab = true;
        }
    }
}