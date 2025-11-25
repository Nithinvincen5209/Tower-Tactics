using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Notify the GameManager
            GameManager.Instance.EnemyCrossed();

            // Destroy the enemy after crossing
            Destroy(other.gameObject);
        }
    }
}
