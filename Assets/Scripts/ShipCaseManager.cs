using UnityEngine;
using TMPro;
using System.Collections;
//using System.Threading.Tasks.Dataflow;

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
    [SerializeField] GameObject[] Ships;

    private int shipIndex = 0;
    public TextMeshPro manifestText;
    public GameObject shipPlaceholder;
    public ScannerTrigger scannerTrigger;

    public ShipCaseData[] shipCases;
    public int currentCaseIndex = 0;

    private bool caseAlreadyDecided = false;
    private bool caseStarted = false;

    public CaseStage currentStage;

    private string enteredCode = "";

    // Logging-only fields
    private float caseStartTime;
    private int wrongCodeSubmissionsThisCase = 0;

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
        if (ExperimentLogger.Instance != null)
        {
            ExperimentLogger.Instance.StartCondition(participantID, conditionName);
        }

        StartCase();
    }

    [Header("Experiment Info")]
    public string participantID = "P001";
    public string conditionName = "Gravity";

    public void StartCase()
    {
        caseAlreadyDecided = false;
        caseStarted = false;
        currentStage = CaseStage.WaitingForKeyCard;
        enteredCode = "";

        // Logging reset
        caseStartTime = Time.time;
        wrongCodeSubmissionsThisCase = 0;

        if (scannerTrigger != null)
        {
            scannerTrigger.ResetScanner();
        }

        if (shipPlaceholder != null)
        {
            Ships[shipIndex].SetActive(false);
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
                for (int i = 0; i < Ships.Length; i++)
                {
                    Ships[i].SetActive(false);
                    if (i == shipIndex)
                    {
                        Ships[i].SetActive(true);
                    }
                }
            }

            ShowManifest();
        }
        else
        {
            wrongCodeSubmissionsThisCase++;

            manifestText.text =
                "Incoming Ship: " + CurrentCase.shipId + "\n" +
                "Incorrect Code\n" +
                "Try Again:\n" +
                enteredCode;

            StartCoroutine(ResetKeypadPromptAfterDelay());
        }
    }

    private IEnumerator ResetKeypadPromptAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (currentStage != CaseStage.WaitingForKeypad)
        {
            yield break;
        }

        enteredCode = "";

        manifestText.text =
            "Incoming Ship: " + CurrentCase.shipId + "\n" +
            "Verification Code: " + CurrentCase.verificationCode + "\n" +
            "Enter code on keypad:\n" +
            "_";
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

        bool decisionCorrect = CurrentCase.shouldApprove;

        if (decisionCorrect)
        {
            manifestText.text = "APPROVED\n\nCorrect decision";
        }
        else
        {
            manifestText.text = "APPROVED\n\nWrong decision";
        }

        if (ExperimentLogger.Instance != null)
        {
            ExperimentLogger.Instance.LogCaseResult(
                currentCaseIndex,
                CurrentCase.shipId,
                Time.time - caseStartTime,
                wrongCodeSubmissionsThisCase,
                "Approve",
                decisionCorrect
            );
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

        bool decisionCorrect = !CurrentCase.shouldApprove;

        if (decisionCorrect)
        {
            manifestText.text = "DENIED\n\nCorrect decision";
        }
        else
        {
            manifestText.text = "DENIED\n\nWrong decision";
        }

        if (ExperimentLogger.Instance != null)
        {
            ExperimentLogger.Instance.LogCaseResult(
                currentCaseIndex,
                CurrentCase.shipId,
                Time.time - caseStartTime,
                wrongCodeSubmissionsThisCase,
                "Deny",
                decisionCorrect
            );
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
        Ships[shipIndex].SetActive(false);

        currentCaseIndex++;
        shipIndex++;

        if (currentCaseIndex >= shipCases.Length)
        {
            currentStage = CaseStage.Complete;

            manifestText.text =
                "Experiment Completed\n\n" +
                "wait for researcher\n" +
                "to give you instructions";

            if (ExperimentLogger.Instance != null)
            {
                ExperimentLogger.Instance.EndCondition();
            }

            return;
        }

        StartCase();
    }
}