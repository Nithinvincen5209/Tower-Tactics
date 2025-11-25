using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public Slider healthSlider;
    public int reward = 30; // coins rewarded on death

    

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        // Keep health bar facing camera (optional, if health bar is a world space UI)
        if (healthSlider != null && healthSlider.gameObject.activeSelf)
        {
            healthSlider.transform.LookAt(Camera.main.transform);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       
        // Reward player coins
        GameManager.Instance.AddCoins(reward);

        // Destroy enemy
        Destroy(gameObject);
    }
}
