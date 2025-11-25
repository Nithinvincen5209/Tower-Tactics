using UnityEngine;
using TMPro;

public class DefenseTimer : MonoBehaviour
{
    public float defenseTime = 60f; // 1 minute
    public TextMeshProUGUI timerText;
    public GameObject gameOverCanvas;
    public GameObject winCanvas;

    public GameManager gameManager;

    private bool gameEnded = false;
    public int coinThreshold = 50; // < 50 triggers fail (with 0 cannons)

    void Start()
    {
        if (gameOverCanvas != null) gameOverCanvas.SetActive(false);
        if (winCanvas != null) winCanvas.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        defenseTime -= Time.deltaTime;
        if (defenseTime < 0) defenseTime = 0;
        UpdateTimerUI();

        // ✅ continuous fail check
        if (CannonManager.Instance != null && gameManager != null)
        {
            if (CannonManager.Instance.ActiveCannons == 0 && gameManager.coins < coinThreshold)
            {
                GameOver();
                return;
            }
        }

        // timer end = win if no fail happened
        if (defenseTime <= 0f && !gameEnded)
        {
            Win();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(defenseTime / 60f);
        int seconds = Mathf.FloorToInt(defenseTime % 60f);
        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;
        if (gameOverCanvas != null) gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over: no cannons on ground AND coins < 50.");
    }

    void Win()
    {
        if (gameEnded) return;
        gameEnded = true;
        if (winCanvas != null) winCanvas.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Player survived the timer.");
    }
}
