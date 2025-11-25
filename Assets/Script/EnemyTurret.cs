using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [Header("References")]
    public Transform rotatingPart;      // the turret head (not the whole body)
    public Transform firePoint;         // where bullet spawns
    public GameObject bulletPrefab;     // enemy bullet prefab

    [Header("Settings")]
    public float fireRate = 1f;         // bullets per second
    public float range = 10f;           // attack range
    public float turnSpeed = 5f;        // turret rotation speed

    private float fireCountdown = 0f;
    private Transform target;
    public AK.Wwise.Event Tank_Shot;
    void Update()
    {
        FindTarget();

        if (target == null) return;

        RotateTowardsTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
    void FindTarget()
    {
        GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestCannon = null;

        foreach (GameObject cannon in cannons)
        {
            float distance = Vector3.Distance(transform.position, cannon.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestCannon = cannon;
            }
        }

        if (nearestCannon != null && shortestDistance <= range)
        {
            target = nearestCannon.transform;
        }
        else
        {
            target = null;
        }
    }


    void RotateTowardsTarget()
    {
        Vector3 dir = target.position - rotatingPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
        rotatingPart.rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void Shoot()
    {
        Tank_Shot.Post(gameObject);
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        EnemyBullet bullet = bulletGO.GetComponent<EnemyBullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
