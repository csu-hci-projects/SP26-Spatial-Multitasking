using UnityEngine;
using System.IO;

public class ExperimentLogger : MonoBehaviour
{
    public static ExperimentLogger Instance;

    [Header("Participant Info")]
    public string participantID = "P001";
    public string conditionName = "Gravity";

    private string caseLogPath;
    private string summaryLogPath;

    private float conditionStartTime;

    private int totalWrongCodeSubmissions = 0;
    private int totalCorrectDecisions = 0;
    private int totalCasesCompleted = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        string folderPath = Application.persistentDataPath;

        caseLogPath = Path.Combine(folderPath, "case_data.csv");
        summaryLogPath = Path.Combine(folderPath, "summary_data.csv");

        if (!File.Exists(caseLogPath))
        {
            File.WriteAllText(caseLogPath,
                "ParticipantID,Condition,CaseIndex,ShipID,CaseTimeSeconds,WrongCodeSubmissions,FinalDecision,DecisionCorrect\n");
        }

        if (!File.Exists(summaryLogPath))
        {
            File.WriteAllText(summaryLogPath,
                "ParticipantID,Condition,TotalConditionTime,TotalWrongCodeSubmissions,TotalCorrectDecisions,CasesCompleted\n");
        }
    }

    public void StartCondition(string participant, string condition)
    {
        participantID = participant;
        conditionName = condition;
        conditionStartTime = Time.time;

        totalWrongCodeSubmissions = 0;
        totalCorrectDecisions = 0;
        totalCasesCompleted = 0;
    }

    public void LogCaseResult(
        int caseIndex,
        string shipID,
        float caseTimeSeconds,
        int wrongCodeSubmissions,
        string finalDecision,
        bool decisionCorrect)
    {
        string row =
            $"{participantID}," +
            $"{conditionName}," +
            $"{caseIndex}," +
            $"{shipID}," +
            $"{caseTimeSeconds:F2}," +
            $"{wrongCodeSubmissions}," +
            $"{finalDecision}," +
            $"{decisionCorrect}\n";

        File.AppendAllText(caseLogPath, row);

        totalWrongCodeSubmissions += wrongCodeSubmissions;

        if (decisionCorrect)
        {
            totalCorrectDecisions++;
        }

        totalCasesCompleted++;
    }

    public void EndCondition()
    {
        float totalConditionTime = Time.time - conditionStartTime;

        string row =
            $"{participantID}," +
            $"{conditionName}," +
            $"{totalConditionTime:F2}," +
            $"{totalWrongCodeSubmissions}," +
            $"{totalCorrectDecisions}," +
            $"{totalCasesCompleted}\n";

        File.AppendAllText(summaryLogPath, row);

        Debug.Log("Case log path: " + caseLogPath);
        Debug.Log("Summary log path: " + summaryLogPath);
    }
}