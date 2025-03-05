using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static WeepingAngel;

public class WandererEnemy : MonoBehaviour
{
    public enum WandererState
    {
        Patrol = 0,
        Chase = 1,
        Idle = 2,
    }
    public WandererState currentState;

    public float timeOutOfSightBeforePatrol;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float patrolSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float killDistance;

    [SerializeField] float maxIdleTime;

    [SerializeField] Transform[] PatrolPoints;
    [SerializeField] Transform currentPatrolPosition;
    [SerializeField] float distanceToSwitchToIdle;

    [SerializeField] Transform PlayerPosition;
    bool isOnWayToPatrolPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        if(isOnWayToPatrolPoint == false)
        {
            int newPatrolPoint = Random.Range(0,PatrolPoints.Length);
            currentPatrolPosition = PatrolPoints[newPatrolPoint];
            isOnWayToPatrolPoint = true;
        }
        agent.destination = currentPatrolPosition.position;
        if(Vector3.Distance(this.transform.position, currentPatrolPosition.position) <= distanceToSwitchToIdle)
        {
            isOnWayToPatrolPoint=false;
            currentState = WandererState.Idle;
            StartCoroutine(RandomIdleDuration());
        }
    }
    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.destination = PlayerPosition.position;
        timeOutOfSightBeforePatrol -= Time.deltaTime;
        if(timeOutOfSightBeforePatrol <= 0)
        {
            currentState = WandererState.Patrol;
            isOnWayToPatrolPoint = false;
        }
        if (Vector3.Distance(PlayerPosition.position, this.transform.position) <= killDistance)
        {
            //play kill animation
            Debug.Log("UrDEad");
        }
    }
    IEnumerator RandomIdleDuration()
    {
        float rand = Random.Range(0, maxIdleTime);
        yield return new WaitForSeconds(rand);
        if(currentState != WandererState.Chase)
        {
            currentState = WandererState.Patrol;
        }
    }
    void Idle()
    {
        agent.speed = 0;
    }
    public void FixedUpdate()
    {
        switch (currentState)
        {
            case WandererState.Patrol:
                Patrol();
                break;
            case WandererState.Idle:
                Idle();
                break;
            case WandererState.Chase:
                Chase();
                break;
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
