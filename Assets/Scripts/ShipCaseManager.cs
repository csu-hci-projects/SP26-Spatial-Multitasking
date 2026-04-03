using UnityEngine;
using TMPro;
using System.Collections;

public enum CaseStage
{
    WaitingForKeyCard,
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

    private string enteredCode = "";

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
        currentStage = CaseStage.WaitingForKeyCard;
        enteredCode = "";

        if (shipPlaceholder != null)
        {
            shipPlaceholder.SetActive(false);
        }

        manifestText.text =
            "Swipe security keycard\n" +
            "to access system";
    }

    public void ActivateKeyCard()
    {
        if (caseStarted || currentStage != CaseStage.WaitingForKeyCard)
        {
            return;
        }

        caseStarted = true;
        currentStage = CaseStage.WaitingForRadio;

        manifestText.text =
            "Pick up the radio\n" +
            "to let in incoming ship";
    }

    public void ActivateRadioCall()
    {
        if (currentStage != CaseStage.WaitingForRadio)
        {
            return;
        }

        currentStage = CaseStage.WaitingForKeypad;
        enteredCode = "";

        manifestText.text =
            "Incoming Ship: " + CurrentCase.shipId + "\n" +
            "Verification Code: " + CurrentCase.verificationCode + "\n" +
            "Enter code on keypad:\n" +
            "_";
    }

    public void AddDigit(string digit)
    {
        if (currentStage != CaseStage.WaitingForKeypad)
        {
            return;
        }

        if (enteredCode.Length >= CurrentCase.verificationCode.Length)
        {
            return;
        }

        enteredCode += digit;

        manifestText.text =
            "Incoming Ship: " + CurrentCase.shipId + "\n" +
            "Verification Code: " + CurrentCase.verificationCode + "\n" +
            "Enter code on keypad:\n" +
            enteredCode;
    }

    public void ClearCode()
    {
        if (currentStage != CaseStage.WaitingForKeypad)
        {
            return;
        }

        enteredCode = "";

        manifestText.text =
            "Incoming Ship: " + CurrentCase.shipId + "\n" +
            "Verification Code: " + CurrentCase.verificationCode + "\n" +
            "Enter code on keypad:\n" +
            "_";
    }

    public void SubmitCode()
    {
        if (currentStage != CaseStage.WaitingForKeypad)
        {
            return;
        }

        if (enteredCode == CurrentCase.verificationCode)
        {
            if (shipPlaceholder != null)
            {
                shipPlaceholder.SetActive(true);
            }

            ShowManifest();
        }
        else
        {
            manifestText.text =
                "Incoming Ship: " + CurrentCase.shipId + "\n" +
                "Incorrect Code\n" +
                "Try Again:\n" +
                enteredCode;
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