using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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

    public UnityEvent KillPlayer;

    public float timeOutOfSightBeforePatrol;
   public SOVentStatus status;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float patrolSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float killDistance;

    [SerializeField] float maxIdleTime;

    [SerializeField] Transform[] PatrolPoints;
    [SerializeField] Transform[] AreaTwoPatrolPoints;
    [SerializeField] Transform currentPatrolPosition;
    [SerializeField] float distanceToSwitchToIdle;

    [SerializeField] Transform PlayerPosition;
    bool isOnWayToPatrolPoint;
    public bool inAreaTwo;
    bool isOnKillCooldown;

    Vector3 StartPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPos = this.transform.position;
    }
    public void ReturnToStartPosition()
    {
        if (this.isActiveAndEnabled)
        {
            agent.enabled = false;
            this.transform.position = StartPos;
            currentState = WandererState.Idle;
            agent.enabled = true;
        }
    }
    public void EnteredAreaTwo()
    {
        inAreaTwo = true;
    }
    public void EnteredAreaOne()
    {
        inAreaTwo = false;
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        if(isOnWayToPatrolPoint == false)
        {
            if (!inAreaTwo)
            {
                int newPatrolPoint = Random.Range(0, PatrolPoints.Length);
                currentPatrolPosition = PatrolPoints[newPatrolPoint];
            }
            else
            {
                int newPatrolPoint = Random.Range(0, AreaTwoPatrolPoints.Length);
                currentPatrolPosition = AreaTwoPatrolPoints[newPatrolPoint];
            }
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
        //need to decide if the wanderer can kill in vents
        if (Vector3.Distance(PlayerPosition.position, this.transform.position) <= killDistance && !status.isVented && !isOnKillCooldown)
        {
            //play kill animation
            Debug.Log("UrDEad");
            StartCoroutine(DeathProcess());
            isOnKillCooldown = true;
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
    IEnumerator DeathProcess()
    {
        AudioManager.PlaySound(0);
        yield return new WaitForSeconds(1.4f);
        AudioManager.PlaySound(2);
        yield return new WaitForSeconds(1.19f);
        AudioManager.PlaySound(3);
        yield return new WaitForSeconds(0.45f);
        KillPlayer.Invoke();
        StartCoroutine(KillCooldown());
        // SceneManager.LoadScene("SafetyBackUp");
    }
    IEnumerator KillCooldown()
    {
        yield return new WaitForSeconds(4);
        isOnKillCooldown = false;
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
