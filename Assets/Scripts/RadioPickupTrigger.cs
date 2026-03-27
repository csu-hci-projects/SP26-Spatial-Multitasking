using UnityEngine;
using Oculus.Interaction;

public class RadioPickupTrigger : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;

    private Grabbable grabbable;
    private bool wasGrabbedLastFrame = false;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable == null || shipCaseManager == null)
        {
            return;
        }

        bool isGrabbedNow = grabbable.SelectingPointsCount > 0;

        if (isGrabbedNow && !wasGrabbedLastFrame)
        {
            shipCaseManager.ActivateRadioCall();
        }

        wasGrabbedLastFrame = isGrabbedNow;
    }
}