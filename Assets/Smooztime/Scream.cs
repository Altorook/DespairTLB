using System.Collections;
using UnityEngine;

public class Scream : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] GameObject[] realMonsters;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var monster in monsters)
        {
            monster.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJumpScare(int monster)
    {
        monsters[monster].SetActive(true);
        realMonsters[monster].transform.GetChild(0).gameObject.SetActive(false);
        animator = monsters[monster].GetComponent<Animator>();
        animator.Play("Base Layer.Scream", 0, 0.0f);
        StartCoroutine(DeativeAnimation(monster));
    }

    private IEnumerator DeativeAnimation(int monster)
    {
        yield return new WaitForSeconds(4);
        monsters[monster].SetActive(false);
        realMonsters[monster].transform.GetChild(0).gameObject.SetActive(true);
    }

}
