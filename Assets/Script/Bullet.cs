using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public GameObject hitEffectPrefab;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // enemy already dead
            return;
        }

        // Move towards enemy
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitTarget(other.transform);
        }
    }

    void HitTarget(Transform hitEnemy)
    {
        // Effect
        if (hitEffectPrefab != null)
        {
            Vector3 SpawnPos = transform.position + Vector3.up * 5f;
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        // Damage enemy
        EnemyHealth enemy = hitEnemy.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject); // always destroy bullet
    }
}
