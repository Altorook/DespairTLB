using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    [SerializeField] private float _timeUntilChange;
    void Start()
    {
        StartCoroutine(GoBackToMainMenu());
    }

    IEnumerator GoBackToMainMenu()
    {
        yield return new WaitForSeconds(_timeUntilChange);
        SceneManager.LoadScene("MainMenu");
    }
}
