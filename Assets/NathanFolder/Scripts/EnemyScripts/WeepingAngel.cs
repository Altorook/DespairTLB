using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WeepingAngel : MonoBehaviour
{
    public enum WeepingState
    {
        HideFromPlayer = 0,
        ScareState = 1,
        Murder = 2,
        LookedAt = 3
    }
    public WeepingState currentState = 0;
    private WeepingState lastStateBeforeLookedAt;

    public UnityEvent KillPlayer;

    private float freezeMult = 1;
    private Animator _anim;
    [SerializeField] NavMeshAgent weepAi;
    //temp var
    [SerializeField] Transform playerPos;
    float speed = 3.5f;
    [SerializeField] SOVentStatus status;
    [SerializeField]
    float killDistance;
    [SerializeField] float stateReturnTimer;
    [SerializeField] float timeToChange;
    [SerializeField] float maxTimeToChange;
    [SerializeField] float minTimeToChange;

    [SerializeField] float resetMaxTime;
    [SerializeField] float resetMinTime;
    [SerializeField] float resetProb;

    [SerializeField] float timeDecreaseFromScare;
    [SerializeField] float murderProbability;
    [SerializeField] float increasePerScareMurderProb;

    [SerializeField] Transform behindPlayerTransform;
    [SerializeField] Transform[] murderSpawnPoints;

    [SerializeField] float maxDistanceBeforeNewPos = 3;
    [SerializeField] float minDistanceBeforeNewPos = .5f;

    [SerializeField] float timeInMurder;
    [SerializeField] float timeBeforeStopMurder;

    public UnityEvent StartJumpScare;

    bool isOnKillCooldown;
    Vector3 StartPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewHideState();
        resetMaxTime = maxTimeToChange;
        resetMinTime = minTimeToChange;
        resetProb  = murderProbability;
        _anim = GetComponentInChildren<Animator>();
        StartPos = this.transform.position;
    }
    IEnumerator WaitToResetPos()
    {
        yield return new WaitForSeconds(4);
        weepAi.enabled = false;
        murderProbability = resetProb;
        maxTimeToChange = resetMaxTime;
        minTimeToChange = resetMinTime;
        NewHideState();
        stateReturnTimer = 0;
        currentState = WeepingState.HideFromPlayer;
        timeInMurder = 0;
        this.transform.position = StartPos;
        weepAi.enabled = true;
    }
        public void ReturnToStartPosition()
    {
        if (this.isActiveAndEnabled)
        {
           StartCoroutine(WaitToResetPos());
        }
    }
    void NewHideState()
    {
        timeToChange = Random.Range(minTimeToChange,maxTimeToChange);
        stateReturnTimer = 0;
        WeepingAngelAudioManager.StopMusic(0);
        this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        currentState = WeepingState.HideFromPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case WeepingState.HideFromPlayer:
                HideFromPlayer();
                break;
            case WeepingState.ScareState:
                Scare();
                break;
            case WeepingState.Murder:
                Murder();
                break;
            case WeepingState.LookedAt:
                LookedAt();
                break;
        }
    }
    public void PlayerCanSee()
    {
        
        if(currentState == WeepingState.ScareState)
        {
            WeepingAngelAudioManager.PlaySound(1);
        }
        //go to hide animation and hold position covering face here
        _anim.SetBool("IsRunning", false);


        lastStateBeforeLookedAt = currentState;
        currentState = WeepingState.LookedAt;
    }
    public void PlayerCannotSee()
    {
        if(lastStateBeforeLookedAt == WeepingState.Murder)
        {
            WeepingAngelAudioManager.PlayMusic(0);
            currentState = lastStateBeforeLookedAt;
        }else if(lastStateBeforeLookedAt == WeepingState.ScareState)
        {
            NewHideState();
        }
    }
    private void LookedAt()
    {
        WeepingAngelAudioManager.StopMusic(0);
        freezeMult = 0;
        weepAi.speed = 0;
        _anim.SetBool("IsRunning", false);
        if (status.isVented)
        {
            timeInMurder += Time.deltaTime;
        }
    }
    IEnumerator DeathProcess()
    {
        KillPlayer.Invoke();
        StartJumpScare.Invoke();
        WeepingAngelAudioManager.PlaySound(0);
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
    private void Murder()
    {

        
        timeInMurder += Time.deltaTime;
        if(timeInMurder > timeBeforeStopMurder)
        {
            murderProbability = resetProb;
            maxTimeToChange = resetMaxTime;
            minTimeToChange = resetMinTime;
            NewHideState();
            stateReturnTimer = 0;
            currentState = WeepingState.HideFromPlayer;
            timeInMurder = 0;
            
        }

        weepAi.speed = speed;
        weepAi.destination = playerPos.position;
        if (Vector3.Distance(playerPos.position, this.transform.position) <= killDistance && !status.isVented && !isOnKillCooldown){
            Debug.Log("UrDEad");
            WeepingAngelAudioManager.StopMusic(0);
            NewHideState();
            StartCoroutine(DeathProcess());
            isOnKillCooldown = true;
        }

        _anim.SetBool("IsRunning", true);
    }
    private void Scare()
    {
        this.transform.LookAt(playerPos.position);
        //maybe play breathing SFX here. or something to slightly hint you are not alone
        if(Vector3.Distance(transform.position,playerPos.position) > maxDistanceBeforeNewPos || Vector3.Distance(transform.position, playerPos.position) < minDistanceBeforeNewPos)
        {
            ScarePosition();
        }
    }
    private void ScarePosition()
    {
        weepAi.enabled = false;
        transform.position = behindPlayerTransform.position;
        currentState = WeepingState.ScareState;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
        weepAi.enabled = true;
    }

    private void HideFromPlayer()
    {
        if (!status.isVented)       
        {
            stateReturnTimer += Time.deltaTime;
        }
        if(stateReturnTimer > timeToChange)
        {
            if(Random.Range(0,1f) < murderProbability)
            {
                WeepingAngelAudioManager.PlayMusic(0);
                currentState = WeepingState.Murder;
                Vector3 furthestSpawnPoint = new Vector3();
                for(int i = 0; i<murderSpawnPoints.Length; i++)
                {
                    if(i == 0)
                    {
                        furthestSpawnPoint = murderSpawnPoints[i].position;
                    }
                    else
                    {
                        Debug.Log(Vector3.Distance(murderSpawnPoints[i].position, playerPos.position) + " " + Vector3.Distance(furthestSpawnPoint, playerPos.position));
                        if (Vector3.Distance(murderSpawnPoints[i].position , playerPos.position) > Vector3.Distance(furthestSpawnPoint , playerPos.position))
                            {
                            furthestSpawnPoint = murderSpawnPoints[i].position;
                            }


                    }
                    
                }
                murderProbability = resetProb;
                maxTimeToChange = resetMaxTime;
                minTimeToChange = resetMinTime;
                this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
                transform.position = furthestSpawnPoint;
            }
            else
            {
                ScarePosition();
                murderProbability += increasePerScareMurderProb;
                minTimeToChange -= timeDecreaseFromScare;
                maxTimeToChange -= timeDecreaseFromScare;
            }
        }
    }
}
