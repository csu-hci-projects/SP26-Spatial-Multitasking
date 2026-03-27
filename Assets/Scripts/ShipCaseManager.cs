using UnityEngine;
using TMPro;
using System.Collections;

public enum CaseStage
{
    WaitingForRadio,
    WaitingForKeypad,
    WaitingForScan,
    WaitingForDecision,
    Complete
}

public class ShipCaseManager : MonoBehaviour
{
    public TextMeshPro manifestText;
    public GameObject shipPlaceholder;

    public ShipCaseData[] shipCases;
    public int currentCaseIndex = 0;

    private bool caseAlreadyDecided = false;
    private bool caseStarted = false;

    public CaseStage currentStage;

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
        currentStage = CaseStage.WaitingForRadio;

        if (shipPlaceholder != null)
        {
            shipPlaceholder.SetActive(false);
        }

        manifestText.text =
            "Pick up the radio\n" +
            "to let in incoming ship";
    }

    public void ActivateRadioCall()
    {
        if (caseStarted || currentStage != CaseStage.WaitingForRadio)
        {
            return;
        }

        caseStarted = true;
        currentStage = CaseStage.WaitingForKeypad;

        manifestText.text =
            "Incoming Ship: " + CurrentCase.shipId + "\n" +
            "Verification Code: " + CurrentCase.verificationCode + "\n" +
            "Status: Enter code on keypad";

        if (shipPlaceholder != null)
        {
            shipPlaceholder.SetActive(true);
        }
    }

    public void ShowManifest()
    {
        if (currentStage != CaseStage.WaitingForKeypad)
        {
            return;
        }

        currentStage = CaseStage.WaitingForScan;

        manifestText.text =
            "Ship ID: " + CurrentCase.shipId + "\n" +
            "Cargo: " + CurrentCase.claimedCargo + "\n" +
            "Status: Awaiting Scan";
    }

    public void ShowScanResult()
    {
        if (currentStage != CaseStage.WaitingForScan)
        {
            return;
        }

        currentStage = CaseStage.WaitingForDecision;

        manifestText.text =
            "Ship ID: " + CurrentCase.shipId + "\n" +
            "Cargo: " + CurrentCase.claimedCargo + "\n" +
            "Scan: " + CurrentCase.actualCargo + "\n" +
            "Status: Awaiting Decision";
    }

    public void ApproveCase()
    {
        if (caseAlreadyDecided || currentStage != CaseStage.WaitingForDecision)
        {
            return;
        }

        caseAlreadyDecided = true;
        currentStage = CaseStage.Complete;

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
        if (caseAlreadyDecided || currentStage != CaseStage.WaitingForDecision)
        {
            return;
        }

        caseAlreadyDecided = true;
        currentStage = CaseStage.Complete;

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