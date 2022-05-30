using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance { get; private set; }
    [Header("In game HUD")]
    [SerializeField] TextMeshProUGUI enemiesKilledCount;
    [Header("Pause menu HUD")]

    [SerializeField] TextMeshProUGUI currentWaveText;
    [Header("Victory Setup HUD")]
    [TextArea] [SerializeField] string congratsMessage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        DisplayCurrentKillCount(0, 0);
        DisplayCurrentWaveNumber(0,0);
        FindObjectOfType<PlayerManager>().SubscribeToGameOver(() => enemiesKilledCount.text = congratsMessage, true);
    }

    void DisplayNewCordLength(float newAmount)
    {
    }
    public void DisplayCurrentKillCount(int enemiesKilled, int allEnemies)
    {
        enemiesKilledCount.text = string.Format("Killed: {0:00}/{1:00}", enemiesKilled, allEnemies);

    }
    public void DisplayCurrentWaveNumber(int currentWave, int waveCount)
    {
        currentWaveText.text = $"Wave: {currentWave} / {waveCount}";
    }


}
