using UnityEngine;

public class DecisionButton : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;
    public bool isApproveButton = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isApproveButton)
        {
            shipCaseManager.ApproveCase();
        }
        else
        {
            shipCaseManager.DenyCase();
        }
    }
}