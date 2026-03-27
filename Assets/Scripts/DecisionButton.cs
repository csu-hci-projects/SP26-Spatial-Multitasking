using UnityEngine;

public class DecisionButton : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;
    public bool isApproveButton = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something touched the button: " + other.name);

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