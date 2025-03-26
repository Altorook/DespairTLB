using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSanity", menuName = "Scriptable Objects/SOSanity")]
public class SOSanity : ScriptableObject
{
    public float maxSanity;
    public float currentSanity;
    public float pillValue;
    public float baseLossMult;

    public float lookingAtEnemyMultiplier;
    public bool isLookingAtEnemy;

    public float noLightMultiplier;
    public bool isLight;
    public float lightMultiplier;

    public float inSafeAreaRegeneration;
    public bool isInSafeArea;

    public float ventMult;
    public bool isVented;

    public float totalMult;
    private void Start()
    {
        currentSanity = 100;
        maxSanity = 100;
        isLookingAtEnemy = false;
        isLight = false;
        isInSafeArea = false;
        isVented = false;
    }
}
