using UnityEngine;
using TMPro;
using System.Collections;

public class ShipCaseManager : MonoBehaviour
{
    public TextMeshPro manifestText;

    public ShipCaseData[] shipCases;

    public int currentCaseIndex = 0;

    private bool caseAlreadyDecided = false;

    public ShipCaseData CurrentCase
    {
        get { return shipCases[currentCaseIndex]; }
    }

    void Start()
    {
        ShowManifest();
    }

    public void ShowManifest()
    {
        caseAlreadyDecided = false;

        manifestText.text =
            "Ship ID: " + CurrentCase.shipId + "\n" +
            "Cargo: " + CurrentCase.claimedCargo + "\n" +
            "Status: Awaiting Scan";
    }

    public void ShowScanResult()
    {
        manifestText.text =
            "Ship ID: " + CurrentCase.shipId + "\n" +
            "Cargo: " + CurrentCase.claimedCargo + "\n" +
            "Scan: " + CurrentCase.actualCargo + "\n" +
            "Status: Awaiting Decision";
    }

    public void ApproveCase()
    {
        if (caseAlreadyDecided)
        {
            return;
        }

        caseAlreadyDecided = true;

        if (CurrentCase.shouldApprove)
        {
            manifestText.text = "APPROVED\n\nCorrect decision";
        }
        else
        {
            manifestText.text = "APPROVED\n\nWrong decision";
        }

        StartCoroutine(LoadNextCaseAfterDelay());
    }

    public void DenyCase()
    {
        if (caseAlreadyDecided)
        {
            return;
        }

        caseAlreadyDecided = true;

        if (!CurrentCase.shouldApprove)
        {
            manifestText.text = "DENIED\n\nCorrect decision";
        }
        else
        {
            manifestText.text = "DENIED\n\nWrong decision";
        }

        StartCoroutine(LoadNextCaseAfterDelay());
    }

    private IEnumerator LoadNextCaseAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        NextCase();
    }

    public void NextCase()
    {
        currentCaseIndex++;

        if (currentCaseIndex >= shipCases.Length)
        {
            currentCaseIndex = 0;
        }

        Debug.Log("Current case index is now: " + currentCaseIndex);
        Debug.Log("Current ship id is now: " + CurrentCase.shipId);

        ShowManifest();
    }
}