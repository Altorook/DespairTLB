using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int lives = 3;
    [SerializeField] Image LivesBackground;
    [SerializeField] TMP_Text LivesText;
    bool isFading = false;
    bool textFadeOut;
    bool textFadeIn;
    public UnityEvent lifeLost;
    public float Opacity = 1;
    [SerializeField] float OpacitySpeed;
    [SerializeField] float TextOpacitySpeed;
    void Start()
    {
        
    }
    public void PlayerDied()
    {
        StartCoroutine(LifeScreen());
    }
    IEnumerator LifeScreen()
    {
       
        if (lives > 0)
        {
            lifeLost.Invoke();
            yield return new WaitForSeconds(2.2f);
            AudioManager.PlaySound(8);
            LivesText.gameObject.SetActive(true);
            LivesBackground.gameObject.SetActive(true);
            LivesBackground.color = new Color(LivesBackground.color.r, LivesBackground.color.g, LivesBackground.color.b,1);
            lives--;
            
            if(lives == 2)
            {
                LivesText.SetText("Three Lives Remain");
            }
            else if(lives == 1)
            {
                LivesText.SetText("Two Lives Remain");
            }else if(lives == 0)
            {
                LivesText.SetText("One Life Remains");
            }
            textFadeOut = true;
            yield return new WaitForSeconds(2f);
            LivesText.SetText("");
            yield return new WaitForSeconds(0.5f);
            textFadeOut= false;
            textFadeIn = true;
            
            if (lives == 2)
            {
                LivesText.SetText("Two Lives Remain");
            }
            else if (lives == 1)
            {
                LivesText.SetText("One Life Remains");
            }
            else if (lives == 0)
            {
                LivesText.SetText("No Lives Remain");
                LivesText.color = new Color(1, 0, 0);
            }
            yield return new WaitForSeconds(3);
            textFadeIn= false;
            Opacity = 1;
            isFading = true;
            yield return new WaitForSeconds(5);
            isFading = false;
            LivesText.gameObject.SetActive(false);
            LivesBackground.gameObject.SetActive(false);
            Opacity = 1;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            yield return null;
        }
            
        
    }
    void FixedUpdate()
    {
     if(isFading)
        {
            Opacity -= Time.deltaTime * OpacitySpeed;
            LivesText.color = new Color(LivesText.color.r, LivesText.color.g, LivesText.color.b,Opacity);
            LivesBackground.color = new Color(LivesBackground.color.r, LivesBackground.color.g, LivesBackground.color.b,Opacity);
        }
        if (textFadeIn)
        {
            Opacity += Time.deltaTime * TextOpacitySpeed;
            LivesText.color = new Color(LivesText.color.r, LivesText.color.g, LivesText.color.b, Opacity);
        }
        if (textFadeOut)
        {
            Opacity -= Time.deltaTime * TextOpacitySpeed;
            LivesText.color = new Color(LivesText.color.r, LivesText.color.g, LivesText.color.b, Opacity);     
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
