using UnityEngine;
using TMPro;
using System.Collections;

public class ShipCaseManager : MonoBehaviour
{
    public TextMeshPro manifestText;
    public GameObject shipPlaceholder;

    public ShipCaseData[] shipCases;

    public int currentCaseIndex = 0;

    private bool caseAlreadyDecided = false;
    private bool caseStarted = false;

    public ShipCaseData CurrentCase
    {
        get { return shipCases[currentCaseIndex]; }
    }
    public bool CaseStarted
    {
        get { return caseStarted; }
    }

    void Start()
    {
        StartCase();
    }

    public void StartCase()
    {
        caseAlreadyDecided = false;
        caseStarted = false;

        shipPlaceholder.SetActive(false);

        manifestText.text =
            "No active ship\n" +
            "Use radio to receive incoming request";
    }

    public void ActivateRadioCall()
    {
        if (caseStarted)
        {
            return;
        }

        caseStarted = true;

        manifestText.text =
            "Incoming Ship: " + CurrentCase.shipId + "\n" +
            "Verification Code: " + CurrentCase.verificationCode + "\n" +
            "Status: Ship Approaching";

        shipPlaceholder.SetActive(true);
    }

    public void ShowManifest()
    {
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

        StartCase();
    }
}