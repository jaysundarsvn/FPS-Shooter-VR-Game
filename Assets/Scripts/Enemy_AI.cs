using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Class for enemy AI that can patrol, chase, and attack
public class Enemy_AI : MonoBehaviour
{
    // Reference to the player game object
    Transform player;
    public int enemyScoreValue = 10;
    public float sightRange, attackRange, wanderRadius, wanderTimer;
    bool inSightRange, inAttackRange, isWandering;

    // Reference to the shooting script
    public Shooting shooting;
    public LayerMask isPlayer;
    public GameObject particleEffect;

    // List of waypoints for patrolling
    List<Transform> points = new List<Transform>();

    // Index of the current destination point
    int destPoint = 0;

    // NavMeshAgent component for navigation
    NavMeshAgent agent;
    private ScoreManager score_manager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("XRPlayerController").transform;
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        // Start wandering
        StartCoroutine(Wander());

        score_manager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        // Check if there are any colliders within the 'sightRange' around this object's position that belong to the 'isPlayer' layer.
        inSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        // Check if there are any colliders within the 'attackRange' around this object's position that belong to the 'isPlayer' layer.
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if(!inSightRange && !inAttackRange)
        {
            // If not chasing or attacking, wander
            if(!isWandering)
            {
                StartCoroutine(Wander());
            }
        }

        else
        {
            // Stop wandering if chasing or attacking
            StopCoroutine(Wander());

            // If in sight range but not attack range, chase
            if (inSightRange && !inAttackRange)
            {
                Chase();
            }
            // If in attack range, attack
            else if (inSightRange && inAttackRange)
            {
                Attack();
            }
        }

        if(GetComponent<Health>().health <= 0)
        {
            Enemy_Death();
            score_manager.PointsToAdd(enemyScoreValue);

        }
    }

    // Patrol function for moving between waypoints
    void Patrol()
    {
        // If there is no path pending and the remaining distance to the destination is less than 0.5f
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // If there are no waypoints, return
            if (points.Count == 0)
            {
                return;
            }

            // Set the destination to the next waypoint
            agent.destination = points[destPoint].position;

            // Increment the destination index and wrap around if necessary
            destPoint = (destPoint + 1) % points.Count;
        }
    }

    // Chase function for pursuing the player
    void Chase()
    {
        // Set the destination to the player's position
        agent.SetDestination(player.position);
    }

    // Attack function for attacking the player
    void Attack()
    {
        // Stop moving and look at the player
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        // Call the Shoot function from the shooting script
        shooting.Shoot();
    }

    // Wander function for random movement
    IEnumerator Wander()
    {
        isWandering = true;

        while (true)
        {
            // Generate a random point within the wander radius
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);

            // Set the destination to the new position
            agent.SetDestination(newPos);

            // Wait for the wander timer before generating a new destination
            yield return new WaitForSeconds(wanderTimer);
        }
    }

    // Generate a random point on the NavMesh within a given radius
    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }

    public void Enemy_Death()
    {
        if(GetComponent<Health>().health <= 0)
        {
            particleEffect.SetActive(true);
            particleEffect.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
