using TMPro;
using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    [Header("Hint Displays")]
    public TMP_Text[] digitHints;
    [SerializeField] TMP_Text[] allHintLocations;
    private string correctCode;
    public string CorrectCode => correctCode;

    private void Start()
    {
        GenerateCode();
    }

    private void GenerateCode()
    {
        int index = 1;
        do
        {
            
            int tempRand = Random.Range(0, allHintLocations.Length);
            if (allHintLocations[tempRand] != null)
            {
                digitHints[index] = allHintLocations[tempRand];
                allHintLocations[tempRand] = null;
                index++;
            }  
        }while(index < digitHints.Length);
           
        
        int code = Random.Range(1111, 10000); // Randomly generates a number from 1111 to 9999
        correctCode = code.ToString();

        Debug.Log("Generated Keypad Code: " + correctCode);


        // Show digits in the world
        if (digitHints != null && digitHints.Length >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                digitHints[i].SetText(((i+1).ToString()) +" - "+ correctCode[i].ToString());
            }
        }
    }
}
