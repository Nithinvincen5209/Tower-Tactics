using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CannonUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI cannonCountText;
    public Image cannonImage;

    [Header("Cannon Settings")]
    public int availableCannons = 2;
    public int coinsPerCannon = 50;

    private int currentCoins = 0;

    // ✅ Track how many are placed
    public int placedCannons = 0;

    void Start()
    {
        UpdateUI();
    }

    // Called when player PLACES a cannon
    public void UseCannon()
    {
        if (availableCannons > 0)
        {
            availableCannons--;
            placedCannons++;   // ✅ increase when cannon is placed
            UpdateUI();
        }
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;

        if (currentCoins <= coinsPerCannon)
        {
            availableCannons++;
            currentCoins -= coinsPerCannon;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        cannonCountText.text = "" + availableCannons;
    }
}
