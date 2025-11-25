using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public GameObject hitEffectPrefab; // add effect prefab

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        // play hit effect slightly above ground
        if (hitEffectPrefab != null)
        {
            Vector3 effectPos = target.position + Vector3.up * 0.5f;
            GameObject effect = Instantiate(hitEffectPrefab, effectPos, Quaternion.identity);
            Destroy(effect, 1f);
        }

        CannonHealth cannon = target.GetComponent<CannonHealth>();
        if (cannon != null)
        {
            cannon.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
