using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HunterAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum HunterState
    {
        Patrol = 0,
        Chase = 1,
        Idle = 2,
    }
    public HunterState currentState;

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

    [SerializeField] float timeNotHunting;
    [SerializeField] float timeTillHunt;
    
    [SerializeField] float minTimeBeforeHunt;
    [SerializeField] float maxTimeBeforeHunt;
    [SerializeField] float percentReductionToHuntMinMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = HunterState.Idle;
        StartCoroutine(RandomIdleDuration());
        timeTillHunt = Random.Range(minTimeBeforeHunt, maxTimeBeforeHunt);
        timeNotHunting = 0;
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        if (isOnWayToPatrolPoint == false)
        {
            int newPatrolPoint = Random.Range(0, PatrolPoints.Length);
            currentPatrolPosition = PatrolPoints[newPatrolPoint];
            isOnWayToPatrolPoint = true;
        }
        agent.destination = currentPatrolPosition.position;
        if (Vector3.Distance(this.transform.position, currentPatrolPosition.position) <= distanceToSwitchToIdle)
        {
            isOnWayToPatrolPoint = false;
            currentState = HunterState.Idle;
            StartCoroutine(RandomIdleDuration());
        }
    }
    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.destination = PlayerPosition.position;

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
        if (currentState != HunterState.Chase)
        {
            currentState = HunterState.Patrol;
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
            case HunterState.Patrol:
                Patrol();
                timeNotHunting += Time.deltaTime;
                break;
            case HunterState.Idle:
                Idle();
                timeNotHunting += Time.deltaTime;
                break;
            case HunterState.Chase:
                Chase();
                break;

        }
        if(timeNotHunting > timeTillHunt)
        {
            currentState = HunterState.Chase;
            minTimeBeforeHunt *= percentReductionToHuntMinMax;
            maxTimeBeforeHunt *= percentReductionToHuntMinMax;
            timeTillHunt = Random.Range(minTimeBeforeHunt, maxTimeBeforeHunt);
            timeNotHunting = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
