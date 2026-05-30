using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialStep
{
    public string stepText;
    public List<GameObject> enableObjects = new();
    public List<GameObject> disableObjects = new();
    public List<Button> activeImages = new();
    public List<Button> deActiveImages = new();
    public Action onStart;
    public Action onComplete;

    public void ExecuteStart()
    {
        // Enable objects
        foreach (var obj in enableObjects)
            if (obj != null) obj.SetActive(true);

        // Disable objects
        foreach (var obj in disableObjects)
            if (obj != null) obj.SetActive(false);

        foreach (var obj in activeImages)
            if(obj !=null ) obj.interactable = true;

        foreach (var obj in deActiveImages)
            if (obj != null) obj.interactable = false;

        onStart?.Invoke();
    }

    public void ExecuteComplete()
    {
        onComplete?.Invoke();
    }
}

public enum TutorialPhase
{
    None,
    MoveOnly,
    BrakeOnly,
    JumpOnly,
    FlipOnly,
    AllEnabled
}
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public static TutorialPhase CurrentPhase { get; private set; } = TutorialPhase.None;
    [SerializeField] private TextMeshProUGUI stepTextUI;

    [SerializeField] private List<TutorialStep> tutorialSteps = new();
    private int currentStepIndex = -1;
    public Button[] allButtons;
    public GameObject[] tutorialButtons;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void StartTutorial()
    {
        currentStepIndex = -1;
        NextStep();
    }

    public void EnableControls()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].interactable = true;
        }
        foreach (var item in tutorialButtons)
        {
            item.SetActive(false);
        }
        PlayerPrefs.SetInt("Tutorial", 1);
        UIManager.Instance.ShowScreen(UIScreenConstants.TutorialDone);
    }
    public void NextStep()
    {
        if (currentStepIndex >= 0 && currentStepIndex < tutorialSteps.Count)
            tutorialSteps[currentStepIndex].ExecuteComplete();

        currentStepIndex++;

        if (currentStepIndex >= tutorialSteps.Count)
        {
            Debug.Log("Tutorial completed.");
            SetPhase(TutorialPhase.AllEnabled);
            EnableControls();
            if (stepTextUI != null)
                stepTextUI.text = "";

            return;
        }

        var step = tutorialSteps[currentStepIndex];

        if (stepTextUI != null)
            stepTextUI.text = step.stepText;

        SetPhaseByStepIndex(currentStepIndex);
        step.ExecuteStart();

        Debug.Log($"Tutorial Step {currentStepIndex + 1}: {step.stepText}");
    }

    private void SetPhaseByStepIndex(int index)
    {
        switch (index)
        {
            case 0:
                SetPhase(TutorialPhase.MoveOnly);
                break;
            case 1:
                SetPhase(TutorialPhase.BrakeOnly);
                break;
            case 2:
                SetPhase(TutorialPhase.JumpOnly);
                break;
            case 3:
                SetPhase(TutorialPhase.FlipOnly);
                break;
            default:
                SetPhase(TutorialPhase.AllEnabled);
                break;
        }
    }

    public void SetPhase(TutorialPhase phase)
    {
        CurrentPhase = phase;
    }

    public void AddEnableObjectToStep(int stepIndex, GameObject obj)
    {
        if (IsValidStepIndex(stepIndex) && obj != null)
            tutorialSteps[stepIndex].enableObjects.Add(obj);
    }

    public void AddDisableObjectToStep(int stepIndex, GameObject obj)
    {
        if (IsValidStepIndex(stepIndex) && obj != null)
            tutorialSteps[stepIndex].disableObjects.Add(obj);
    }

    public void SetOnStartAction(int stepIndex, Action action)
    {
        if (IsValidStepIndex(stepIndex))
            tutorialSteps[stepIndex].onStart = action;
    }

    public void SetOnCompleteAction(int stepIndex, Action action)
    {
        if (IsValidStepIndex(stepIndex))
            tutorialSteps[stepIndex].onComplete = action;
    }

    private bool IsValidStepIndex(int index)
    {
        return index >= 0 && index < tutorialSteps.Count;
    }
}
