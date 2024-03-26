using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public KeyCode flashLight;
    public bool isFlashlightOn;
    	public AudioSource FlashlightSound;
       	public AudioClip[] Flashlight_OnOff;
   // public Material mat1;
    	
    void Update()
    {

        if (Input.GetKeyDown(flashLight)) // Toggle flashlight
        {
            isFlashlightOn = !isFlashlightOn;

            if (isFlashlightOn == true)
            {
                flashlight.SetActive(true);
                FlashlightSound.clip = Flashlight_OnOff[0];
                FlashlightSound.Play();
                // Enable emission
              //  mat1.EnableKeyword("_EMISSION");
            }
            if (isFlashlightOn == false)
            {
                flashlight.SetActive(false);
                FlashlightSound.clip = Flashlight_OnOff[1];
                FlashlightSound.Play();
             //   mat1.DisableKeyword("_EMISSION");
            }

        }

    }

}
