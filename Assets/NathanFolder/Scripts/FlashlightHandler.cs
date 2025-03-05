using UnityEngine;
using UnityEngine.UI;

public class FlashlightHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool flashlightIsOn;
    [SerializeField] float MaxFlashlightCharge;
    [SerializeField] float currentFlashlightCharge;
    [SerializeField] float flashlightChargeDrainSpeed;
    [SerializeField] float flashLightChargeRate;
    [SerializeField] Light flashlightLight;
    [SerializeField] Light flashlightOuterLight;
    [SerializeField] Slider ChargeDisplay;
    bool isMenuOpen;
    void Start()
    {
        
    }
    public void ToggleFlashlight()
    {
        flashlightIsOn = !flashlightIsOn;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isMenuOpen == false)
        {
            ToggleFlashlight();
        }
       
    }
    private void ChargeFlashLight()
    {
        if(currentFlashlightCharge < MaxFlashlightCharge)
        {
            flashlightIsOn = false;
            currentFlashlightCharge += flashLightChargeRate * .1f;
            ChargeDisplay.value = (currentFlashlightCharge / MaxFlashlightCharge);

        }
        else
        {
            currentFlashlightCharge = MaxFlashlightCharge;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1) && isMenuOpen == false)
        {
            ChargeFlashLight();
        }
        if (flashlightIsOn && currentFlashlightCharge > 0)
        {
            currentFlashlightCharge -= flashlightChargeDrainSpeed * .1f;
            ChargeDisplay.value = (currentFlashlightCharge / MaxFlashlightCharge);
            flashlightLight.enabled = true;
            flashlightOuterLight.enabled = true;
            if(currentFlashlightCharge <= 0)
            {
                flashlightIsOn = false;
            }
        }
        else
        {
            flashlightLight.enabled= false;
            flashlightOuterLight.enabled= false;

        }
    }
    public void ToggleMenuState()
    {
        isMenuOpen = !isMenuOpen;
    }
}
