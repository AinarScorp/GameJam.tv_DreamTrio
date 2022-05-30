using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationMessage : MonoBehaviour
{

    public static ConfirmationMessage Instance { get; private set; }

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

    }
    public void ConfirmationQuestion(string questionText, Action yesAction, Action noAction, string yesText = "Yes", string noText = "No")
    {
        RemoveListeners();

        ToggleQuestion(true);
        confirmationText.text = questionText.ToUpper();
        yesBtnText.text = yesText;
        noBtnText.text = noText;
        yesBtn.onClick.AddListener(() =>
        {
            ToggleQuestion(false);

            yesAction?.Invoke();

        });
        noBtn.onClick.AddListener(() =>
        {

            ToggleQuestion(false);
            noAction?.Invoke();

        });
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
