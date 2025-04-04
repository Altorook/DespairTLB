using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOSanity soSanity;
    [SerializeField] Volume Volume;
    VolumeProfile volprof;
    [SerializeField] float maxFilmGrain;
    [SerializeField] float maxChromaticAb;
    [SerializeField] float maxDistortion;
    [SerializeField] float maxWhiteBal;

    void Start()
    {
        volprof = Volume.sharedProfile;
        soSanity.currentSanity = soSanity.maxSanity;
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
        if (soSanity.isLight || soSanity.isFlashLight)
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
            if(soSanity.currentSanity <= soSanity.maxSanity)
            {
                soSanity.currentSanity += Time.deltaTime * soSanity.inSafeAreaRegeneration;
            }
        }
            if (volprof.TryGet<FilmGrain>(out var FG))
            {

                FG.intensity.Override(Mathf.Lerp(0,maxFilmGrain,1-(soSanity.currentSanity/soSanity.maxSanity)));
            }
            if (volprof.TryGet<ChromaticAberration>(out var ChromaticAberration))
            {
                ChromaticAberration.intensity.Override(Mathf.Lerp(0, maxChromaticAb, 1 - (soSanity.currentSanity / soSanity.maxSanity)));
            }
            if (volprof.TryGet<LensDistortion>(out var LD))
            {
                LD.intensity.Override(Mathf.Lerp(0, maxDistortion, 1 - (soSanity.currentSanity / soSanity.maxSanity)));
            }
        if (volprof.TryGet<WhiteBalance>(out var whiteBal))
        {
            whiteBal.temperature.Override(Mathf.Lerp(0, maxWhiteBal, 1 - (soSanity.currentSanity / soSanity.maxSanity)));
        }

    }
}
