using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float idleSpeed = 4f;
    public float chaseSpeed = 10f;
    public float detectionRadius = 16f;
    public float damagePerSecond = 10f;
    public float wanderRadius = 32f;
    public float wanderTime = 5f;
    public float rotationSpeed = 5f; 

    private Transform player;
    private bool isChasing = false;
    private Vector3 wanderTarget;
    private float wanderTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        wanderTimer = wanderTime;
        SetRandomWanderTarget();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (!isChasing)
            {
                isChasing = true;
            }
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
            }
            Wander();
        }
    }

    void ChasePlayer()
    {
        // Rotate to face the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    void Wander()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0)
        {
            SetRandomWanderTarget();
            wanderTimer = wanderTime;
        }

        // Rotate to face the wander target
        Vector3 directionToTarget = (wanderTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the wander target
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, idleSpeed * Time.deltaTime);
    }

    void SetRandomWanderTarget()
    {
        // Set a random target within the wander radius around the enemy's current position
        float randomX = Random.Range(-wanderRadius, wanderRadius);
        float randomZ = Random.Range(-wanderRadius, wanderRadius);
        wanderTarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Keep the enemy on the "terrain" by clamping the target to the terrain bounds
        GameObject terrain = GameObject.Find("terrain");
        if (terrain != null)
        {
            Renderer terrainRenderer = terrain.GetComponent<Renderer>();
            wanderTarget.x = Mathf.Clamp(wanderTarget.x, terrainRenderer.bounds.min.x, terrainRenderer.bounds.max.x);
            wanderTarget.z = Mathf.Clamp(wanderTarget.z, terrainRenderer.bounds.min.z, terrainRenderer.bounds.max.z);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Inflict damage continuously as long as the enemy collides with the player
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
