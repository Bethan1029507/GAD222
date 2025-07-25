using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public int oblivionChoices = 0;
    public int submissionChoices = 0;
    public int rebirthChoices = 0;

    public bool talkedToReceptionist = false;
    public bool talkedToWhisperingPatient = false;
    public bool talkedToDoctor = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public string DetermineEnding()
    {
        if (oblivionChoices >= 2) return "Oblivion";
        if (submissionChoices >= 2) return "Submission";
        if (rebirthChoices >= 1 && oblivionChoices <= 2) return "Rebirth";
        return "Submission"; //default
    }
}
