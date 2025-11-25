using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;   // Assigned by spawner
    public float speed = 5f;
    public float turnSpeed = 5f;    // smooth turning speed

    private int waypointIndex = 0;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position; // start at first waypoint
            waypointIndex = 1; // next target is waypoint 1
        }
    }

    void Update()
    {
        if (waypoints.Length == 0 || waypointIndex >= waypoints.Length)
            return;

        // target position
        Vector3 targetPos = waypoints[waypointIndex].position;

        // keep van grounded (y stays same as current)
        targetPos.y = transform.position.y;

        // direction to next waypoint
        Vector3 dir = (targetPos - transform.position).normalized;

        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Smoothly rotate towards next waypoint
        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * turnSpeed);
        }

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            waypointIndex++;
        }
    }
}
