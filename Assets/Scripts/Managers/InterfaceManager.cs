using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance { get; private set; }
    [Header("In game HUD")]
    [SerializeField] TextMeshProUGUI enemiesKilledCount;
    [Header("Pause menu HUD")]

    [SerializeField] TextMeshProUGUI currentWaveText;

    [Header("Confirmation Question")]
    [SerializeField] TextMeshProUGUI confirmationText;
    [SerializeField] Button yesBtn, noBtn;
    [SerializeField] GameObject confirmationWindow;
    TextMeshProUGUI yesBtnText, noBtnText;

    private void Awake()
    {
        Instance = this;
        yesBtnText = yesBtn.GetComponent<TextMeshProUGUI>();
        noBtnText = noBtn.GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        ToggleQuestion(false);
        DisplayCurrentKillCount(0, 0);
        DisplayCurrentWaveNumber(0);
    }

    public void DisplayNewCordLength(float newAmount)
    {
    }
    public void DisplayCurrentKillCount(int enemiesKilled, int allEnemies)
    {
        enemiesKilledCount.text = string.Format("Killed: {0:00}/{1:00}", enemiesKilled, allEnemies);

    }
    public void DisplayCurrentWaveNumber(int currentWave)
    {
        currentWaveText.text = $"Wave: {currentWave}";
    }

    public void ConfirmationQuestion(string questionText, Action yesAction, Action noAction, string yesText = "Yes", string noText = "No")
    {
        ToggleQuestion(true);
        confirmationText.text = questionText.ToUpper();
        yesBtnText.text = yesText;
        noBtnText.text = noText;
        yesBtn.onClick.AddListener(() =>
        {
            ToggleQuestion(false);

            yesAction?.Invoke();
            RemoveListeners();

        });
        noBtn.onClick.AddListener(() =>
        {

            ToggleQuestion(false);
            noAction?.Invoke();
            RemoveListeners();
        });
        //make it appear
    }
    void RemoveListeners()
    {
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();
    }
    void ToggleQuestion(bool switchTo)
    {
        confirmationWindow.SetActive(switchTo);
    }
}
