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
    public UnityEvent StartJumpScare;

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
    private Animator _anim;

    Vector3 StartPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPos = this.transform.position;
        _anim = GetComponentInChildren<Animator>();
        StartCoroutine(RandomGruntNoises());
    }
    IEnumerator WaitToResetPos()
    {
        yield return new WaitForSeconds(4);
        agent.enabled = false;
        this.transform.position = StartPos;
        currentState = WandererState.Idle;
        agent.enabled = true;
    }
        public void ReturnToStartPosition()
    {
        if (this.isActiveAndEnabled)
        {
           StartCoroutine(WaitToResetPos());
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

        _anim.SetBool("IsPatrolling", true);
        _anim.SetBool("IsChase", false);
        _anim.SetBool("CanAttack", false);

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
            WandererAudioManager.StopWalkingSound();
            isOnWayToPatrolPoint =false;
            currentState = WandererState.Idle;
            StartCoroutine(RandomIdleDuration());
        }
    }
    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.destination = PlayerPosition.position;
        timeOutOfSightBeforePatrol -= Time.deltaTime;

        _anim.SetBool("IsPatrolling", false);
        _anim.SetBool("IsChase", true);
        _anim.SetBool("CanAttack", false);

        if (timeOutOfSightBeforePatrol <= 0)
        {
            currentState = WandererState.Patrol;
            isOnWayToPatrolPoint = false;
        }
        //need to decide if the wanderer can kill in vents
        if (Vector3.Distance(PlayerPosition.position, this.transform.position) <= killDistance && !status.isVented && !isOnKillCooldown)
        {
            //play kill animation
            _anim.SetBool("IsPatrolling", false);
            _anim.SetBool("IsChase", false);
            _anim.SetBool("CanAttack", true);
            WandererAudioManager.StopWalkingSound();

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
            WandererAudioManager.PlayWalkingSound();
            currentState = WandererState.Patrol;
        }
    }
    void Idle()
    {
        agent.speed = 0;

        _anim.SetBool("IsPatrolling", false);
        _anim.SetBool("IsChase", false);
        _anim.SetBool("CanAttack", false);
    }
    IEnumerator DeathProcess()
    {

        yield return new WaitForSeconds(1.1f);
        KillPlayer.Invoke();
        StartJumpScare.Invoke();
        WandererAudioManager.PlaySound(3);
        yield return new WaitForSeconds(1.4f);
        yield return new WaitForSeconds(1.19f);
        yield return new WaitForSeconds(0.45f);
        
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
    
    IEnumerator RandomGruntNoises()
    {
        yield return new WaitForSeconds(Random.Range(4, 13.1f));
        if (!isOnKillCooldown)
        {
            WandererAudioManager.PlaySound(Random.Range(0, 3));
            StartCoroutine(RandomGruntNoises());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
