using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform turretHead; // the rotating part

    private Transform target;
    public AK.Wwise.Event Cannon_Shot;

    void Update()
    {
        FindTarget();

        if (target == null) return;

        // Rotate only the turret head
        Vector3 dir = target.position - turretHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        turretHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Check if enemy is actually inside range
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= range)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Only assign target if inside range
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null && target != null)
        {
            bullet.SetTarget(target);
            Cannon_Shot.Post(gameObject);
        }
    }

    // Draw turret range in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
