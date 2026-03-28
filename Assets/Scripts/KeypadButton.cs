using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public ShipCaseManager shipCaseManager;
    public string buttonValue;
    public bool isEnterButton = false;
    public bool isClearButton = false;

    private bool canPress = true;
    private float pressCooldown = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!canPress)
        {
            return;
        }

        if (!other.CompareTag("IndexFinger"))
        {
            return;
        }

        if (shipCaseManager == null)
        {
            return;
        }

        canPress = false;
        Invoke(nameof(ResetPress), pressCooldown);

        if (isEnterButton)
        {
            shipCaseManager.SubmitCode();
            return;
        }

        if (isClearButton)
        {
            shipCaseManager.ClearCode();
            return;
        }

        shipCaseManager.AddDigit(buttonValue);
    }

    private void ResetPress()
    {
        canPress = true;
    }
}