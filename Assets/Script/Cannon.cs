using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [Header("Cannon Stats")]
    public int maxHealth = 3;
    public int damage = 1;

    [Header("Upgrade Settings")]
    public int upgradeCost = 50;
    public int upgradeDamageIncrease = 2;
    public int upgradeHealthIncrease = 2;

    [Header("UI")]
    public Button upgradeButton;   // assign in inspector
    public TextMeshProUGUI upgradeText;       // optional text on button

    [Header("Effects")]
    public GameObject upgradeEffectPrefab;   // assign particle effect prefab here

    private int currentHealth;
    private bool isUpgraded = false;

    [Header("Sounds")]
    public AK.Wwise.Event CannonUpgrade;

    void Start()
    {
        currentHealth = maxHealth;

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeCannon);
            upgradeButton.gameObject.SetActive(false); // hidden until affordable
        }
    }

    void Update()
    {
        // Check coins and enable button if conditions met
        if (upgradeButton != null && !isUpgraded)
        {
            if (GameManager.Instance.coins >= upgradeCost)
                upgradeButton.gameObject.SetActive(true);
            else
                upgradeButton.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void UpgradeCannon()
    {
        if (isUpgraded) return;

        if (GameManager.Instance.SpendCoins(upgradeCost))
        {
            // Apply upgrades
            maxHealth += upgradeHealthIncrease;
            currentHealth = maxHealth;
            damage += upgradeDamageIncrease;

            isUpgraded = true;

            // Spawn upgrade effect
            if (upgradeEffectPrefab != null)
            {
                Instantiate(upgradeEffectPrefab, transform.position, Quaternion.identity);
            }
            if(CannonUpgrade != null)
            {
                CannonUpgrade.Post(gameObject);
            }

            // Disable upgrade button for this cannon
            if (upgradeButton != null)
            {
                upgradeButton.gameObject.SetActive(false);
            }

            if (upgradeText != null)
                upgradeText.text = "Upgraded!";
        }
    }
}
