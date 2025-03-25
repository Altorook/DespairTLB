using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] GameObject weepObj;
    [SerializeField] GameObject huntObj;
    [SerializeField] GameObject wanderObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnWeeping()
    {
        weepObj.SetActive(true);
    }
    public void SpawnHunter()
    {
        huntObj.SetActive(true);

    }
    public void SpawnWanderer()
    {
        wanderObj.SetActive(true);

    }
}
