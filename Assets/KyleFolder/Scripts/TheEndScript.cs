using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collsion)
    {
        if(collsion.gameObject.layer == 11)
        {
            SceneManager.LoadScene("GameComplete");
        }
    }
}
