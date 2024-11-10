using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float idleSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRadius = 16f;
    public float damagePerSecond = 5f;
    public float wanderRadius = 10f;
    public float wanderTime = 3f;
    public float rotationSpeed = 5f; // Speed at which the enemy rotates towards the player

    private Transform player;
    private bool isChasing = false;
    private float attackTimer = 0f;
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

        // Check if the enemy is close enough to attack the player
        if (Vector3.Distance(transform.position, player.position) < 1.5f)
        {
            AttackPlayer();
        }
        else
        {
            attackTimer = 0f;
        }
    }

    void Wander()
    {
        // Update wander target if timer has reached zero
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

    void AttackPlayer()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1f)
        {
            attackTimer = 0f;
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond);
            }
        }
    }
}
