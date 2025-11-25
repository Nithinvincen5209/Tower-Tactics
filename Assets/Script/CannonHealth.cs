using UnityEngine;
using UnityEngine.UI;

public class CannonHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject destroyEffect;
    public Slider healthSlider;
    public int coinPenaltyOnDestroy = 40;
    public AK.Wwise.Event Explosion;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthSlider != null) healthSlider.value = currentHealth;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        // Play explosion sound
        Explosion.Post(gameObject);
        if (destroyEffect != null)
            Instantiate(destroyEffect, transform.position, Quaternion.identity);

        // optional penalty
        if (GameManager.Instance != null)
            GameManager.Instance.SpendCoins(coinPenaltyOnDestroy);

        // ✅ mark as inactive
        if (CannonManager.Instance != null)
            CannonManager.Instance.UnregisterCannon();

        Destroy(gameObject);
    }
}
