using UnityEngine;

[System.Serializable]
public class ShipCaseData
{
    public string shipId;
    public string verificationCode;
    public string claimedCargo;
    public string actualCargo;
    public bool shouldApprove;
}