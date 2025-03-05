using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    private float freezeMult = 1;
    [SerializeField] NavMeshAgent weepAi;
    //temp var
    [SerializeField] Transform playerPos;
    float speed = 3.5f;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewHideState();
        resetMaxTime = maxTimeToChange;
        resetMinTime = minTimeToChange;
        resetProb  = murderProbability;
    }
    void NewHideState()
    {
        timeToChange = Random.Range(minTimeToChange,maxTimeToChange);
        stateReturnTimer = 0;
        this.gameObject.GetComponent<Renderer>().enabled = false;
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
            //play sound for a jump here 
        }
        //go to hide animation and hold position covering face here


        lastStateBeforeLookedAt = currentState;
        currentState = WeepingState.LookedAt;
    }
    public void PlayerCannotSee()
    {
        if(lastStateBeforeLookedAt == WeepingState.Murder)
        {
            currentState = lastStateBeforeLookedAt;
        }else if(lastStateBeforeLookedAt == WeepingState.ScareState)
        {
            NewHideState();
        }
    }
    private void LookedAt()
    {
        freezeMult = 0;
        weepAi.speed = 0;
    }
    private void Murder()
    {

        //play stone scraping sound here


        weepAi.speed = speed;
        weepAi.destination = playerPos.position;
        if (Vector3.Distance(playerPos.position, this.transform.position) <= killDistance){
            Debug.Log("UrDEad");
        }
    }
    private void Scare()
    {
        //maybe play breathing SFX here. or something to slightly hint you are not alone
        if(Vector3.Distance(transform.position,playerPos.position) > maxDistanceBeforeNewPos || Vector3.Distance(transform.position, playerPos.position) < minDistanceBeforeNewPos)
        {
            ScarePosition();
        }
    }
    private void ScarePosition()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        this.gameObject.GetComponent<Renderer>().enabled = true;
        transform.position = behindPlayerTransform.position;
        currentState = WeepingState.ScareState;
    }
    private void HideFromPlayer()
    {
        stateReturnTimer += Time.deltaTime;
        if(stateReturnTimer > timeToChange)
        {
            if(Random.Range(0,1f) < murderProbability)
            {
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
                this.gameObject.GetComponent<Renderer>().enabled = true;
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
