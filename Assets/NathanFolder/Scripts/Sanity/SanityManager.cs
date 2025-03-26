using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOSanity soSanity;
    [SerializeField] Volume Volume;
    VolumeProfile volprof;

    void Start()
    {
        volprof = Volume.sharedProfile;
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
        if(soSanity.currentSanity >= soSanity.maxSanity /2)
        {
            if (volprof.TryGet<FilmGrain>(out var FG))
            {

                FG.intensity.Override(0f);
            }
            if (volprof.TryGet<ChromaticAberration>(out var ChromaticAberration))
            {
                ChromaticAberration.intensity.Override(0f);
            }
            if (volprof.TryGet<LensDistortion>(out var LD))
            {
                LD.intensity.Override(0f);
            }
            if (volprof.TryGet<ColorAdjustments>(out var CA))
            {
                CA.saturation.Override(0);
            }
        }
        if (soSanity.currentSanity < soSanity.maxSanity / 2 && soSanity.currentSanity > soSanity.maxSanity / 3)
        {
            if(volprof.TryGet<FilmGrain>(out var FG))
            {
             
                FG.intensity.Override(0.5f);
            }
            if(volprof.TryGet<ChromaticAberration>(out var ChromaticAberration))
            {
                ChromaticAberration.intensity.Override(0.5f);
            }
            if (volprof.TryGet<LensDistortion>(out var LD))
            {
                LD.intensity.Override(-0.0f);
            }
            if(volprof.TryGet<ColorAdjustments>(out var CA))
            {
                CA.saturation.Override(-0.5f);
            }
        }
        if (soSanity.currentSanity < soSanity.maxSanity / 3 && soSanity.currentSanity > soSanity.maxSanity / 5)
        {
            if (volprof.TryGet<FilmGrain>(out var FG))
            {

                FG.intensity.Override(0.7f);
            }
            if (volprof.TryGet<ChromaticAberration>(out var ChromaticAberration))
            {
                ChromaticAberration.intensity.Override(0.7f);
            }
            if (volprof.TryGet<LensDistortion>(out var LD))
            {
                LD.intensity.Override(-0.2f);
            }
            if (volprof.TryGet<ColorAdjustments>(out var CA))
            {
                CA.saturation.Override(-0.7f);
            }
        }
        if (soSanity.currentSanity < soSanity.maxSanity / 5)
        {
            if (volprof.TryGet<FilmGrain>(out var FG))
            {

                FG.intensity.Override(1f);
            }
            if (volprof.TryGet<ChromaticAberration>(out var ChromaticAberration))
            {
                ChromaticAberration.intensity.Override(1f);
            }
            if (volprof.TryGet<LensDistortion>(out var LD))
            {
                LD.intensity.Override(-0.4f);
            }
            if (volprof.TryGet<ColorAdjustments>(out var CA))
            {
                CA.saturation.Override(-1f);
            }
        }
    }
}
