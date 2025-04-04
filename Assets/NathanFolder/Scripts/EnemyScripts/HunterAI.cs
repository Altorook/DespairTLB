using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

    public UnityEvent KillPlayer;
    public UnityEvent StartJumpScare;

    [SerializeField] SOVentStatus status;
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

    [SerializeField] float timeNotHunting;
    [SerializeField] float timeTillHunt;
    
    [SerializeField] float minTimeBeforeHunt;
    [SerializeField] float maxTimeBeforeHunt;
    


    [SerializeField] float huntDuration;
    [SerializeField] float timeInHunt;
    [SerializeField] float percentReductionToHuntMinMax;
    public bool inAreaTwo;

    bool isOnKillCooldown;
    bool isWait;
    private Animator _anim;

    Vector3 StartPos;
    public void EnteredAreaTwo()
    {
        inAreaTwo = true;
    }
    public void EnteredAreaOne()
    {
        inAreaTwo = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnterIdle();
        timeTillHunt = Random.Range(minTimeBeforeHunt, maxTimeBeforeHunt);
        timeNotHunting = 0;
        StartPos = this.transform.position;
        _anim = GetComponentInChildren<Animator>();
        HunterAudioManager.PlayMusic(1);
    }
    IEnumerator WaitToResetPos()
    {
        yield return new WaitForSeconds(4);
        agent.enabled = false;
        this.transform.position = StartPos;
        EnterIdle();
        timeTillHunt = Random.Range(minTimeBeforeHunt, maxTimeBeforeHunt);
        timeNotHunting = 0;

        agent.enabled = true;
    }
    public void ReturnToStartPosition()
    {
        if (this.isActiveAndEnabled)
        {
            StartCoroutine(WaitToResetPos());
        }
        
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        _anim.SetBool("IsWalking", true);
        _anim.SetBool("IsChasing", false);
        if (isOnWayToPatrolPoint == false)
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
        if (Vector3.Distance(this.transform.position, currentPatrolPosition.position) <= distanceToSwitchToIdle)
        {
            isOnWayToPatrolPoint = false;
            EnterIdle();
        }
    }
    void Chase()
    {
        if (!isWait)
        {
            agent.speed = chaseSpeed;
        }
        else
        {
            agent.speed = 0;
        }
        agent.destination = PlayerPosition.position;
        if(status.isVented)
        {
            timeInHunt += Time.deltaTime * 2.5f;
        }
        else
        {
            timeInHunt += Time.deltaTime;
        }
       
        _anim.SetBool("IsWalking", false);
        _anim.SetBool("IsChasing", true);
        if (timeInHunt >= huntDuration)
        {
            
            timeInHunt = 0;
            EnterIdle();
            HunterAudioManager.PlayMusic(1);
        }
        if (Vector3.Distance(PlayerPosition.position, this.transform.position) <= killDistance &&!status.isVented && !isOnKillCooldown)
        {
            //play kill animation
            Debug.Log("UrDEad");
            StartCoroutine(DeathProcess());
            isOnKillCooldown = true;
        }
    }
    IEnumerator DeathProcess()
    {
        KillPlayer.Invoke();
        StartJumpScare.Invoke();
        HunterAudioManager.PlaySound(0);    
        yield return new WaitForSeconds(1.4f);
        yield return new WaitForSeconds(1.19f);
        yield return new WaitForSeconds(0.45f);
    
        StartCoroutine(KillCooldown());
        //SceneManager.LoadScene("SafetyBackUp");
    }
    IEnumerator KillCooldown()
    {
        yield return new WaitForSeconds(4);
        isOnKillCooldown = false;
    }
    public void EnterIdle()
    {
        currentState = HunterState.Idle;
        StartCoroutine(RandomIdleDuration());
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
        _anim.SetBool("IsWalking", false);
        _anim.SetBool("IsChasing", false);
    }
    public void FixedUpdate()
    {
        switch (currentState)
        {
            case HunterState.Patrol:
                Patrol();
                if (!status.isVented)
                {
                    timeNotHunting += Time.deltaTime;
                }
                break;
            case HunterState.Idle:
                Idle();
                if (!status.isVented)
                {
                    timeNotHunting += Time.deltaTime;
                }
                break;
            case HunterState.Chase:
                Chase();
                break;

        }
        if(timeNotHunting > timeTillHunt)
        {
            StartCoroutine(PreapareChasing());
            HunterAudioManager.PlayMusic(0);
            minTimeBeforeHunt *= percentReductionToHuntMinMax;
            maxTimeBeforeHunt *= percentReductionToHuntMinMax;
            timeTillHunt = Random.Range(minTimeBeforeHunt, maxTimeBeforeHunt);
            timeNotHunting = 0;
        }
    }

    private IEnumerator PreapareChasing()
    {
        currentState = HunterState.Chase;
        isWait = true;
        yield return new WaitForSeconds(2.8f);
        isWait = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
