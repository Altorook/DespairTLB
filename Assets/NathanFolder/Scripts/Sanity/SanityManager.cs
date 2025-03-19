using UnityEditor.Build;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOSanity soSanity;
    void Start()
    {
        
    }
    public void TakePills()
    {
        soSanity.currentSanity += soSanity.pillValue;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        soSanity.totalMult = soSanity.baseLossMult;
        if (soSanity.isLookingAtEnemy)
        {
            soSanity.totalMult += soSanity.lookingAtEnemyMultiplier;
        }
        if (soSanity.isLight)
        {
            soSanity.totalMult -= soSanity.lightMultiplier;
        }
        else
        {
            soSanity.totalMult += soSanity.noLightMultiplier;
        }
        if(soSanity.isVented)
        {
            soSanity.totalMult -= soSanity.ventMult;
        }
        if (!soSanity.isInSafeArea)
        {
            soSanity.currentSanity -= Time.deltaTime * soSanity.totalMult;
        }
        else
        {
            soSanity.currentSanity += Time.deltaTime * soSanity.inSafeAreaRegeneration;
        }
    }
}
