using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Coins")]
    public int coins = 100;
    public TextMeshProUGUI coinText;

    [Header("Lives System")]
    public int maxLives = 4;
    private int currentLives;
    public Image[] lifeImages;   // drag your 4 green UI images here

    [Header("Game Over")]
    public GameObject gameOverCanvas;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        UpdateCoinUI();

        // Setup lives
        currentLives = maxLives;
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        foreach (var img in lifeImages)
            img.gameObject.SetActive(true);
    }

    // --- COIN SYSTEM ---
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinUI();
            return true;
        }
        return false; // not enough coins
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = ":" + coins;
    }

    // --- LIVES SYSTEM ---
    public void EnemyCrossed()
    {
        currentLives--;

        Debug.Log("Enemy crossed! Lives left: " + currentLives);

        // Disable one green image
        if (currentLives >= 0 && currentLives < lifeImages.Length)
        {
            lifeImages[currentLives].gameObject.SetActive(false);
        }

        // No lives left -> Game Over
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Time.timeScale = 0f; // pause game
    }
}
