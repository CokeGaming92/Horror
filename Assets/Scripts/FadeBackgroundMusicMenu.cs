using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBackgroundMusicMenu : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Duration over which the volume fades
    public float targetVolume = 0.5f; // Target volume level
    public float decreaseDuration = 1.0f; // Duration over which the volume decreases
    public float targetDecreaseVolume = 0.1f; // Target volume to decrease to
 

    public AudioSource audioSource;
    private float initialVolume;
    private float currentFadeTime = 0.0f;
   

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        initialVolume = audioSource.volume; // Store the initial volume



    }

    // Method to fade the volume when invoked
    public void FadeVolume()
    {
        // Reset the fade time
        currentFadeTime = 0.0f;
     

        // Start fading the volume
        StartCoroutine(FadeVolumeCoroutine());
    }

    // Coroutine for fading the volume
    private IEnumerator FadeVolumeCoroutine()
    {
        while (currentFadeTime < fadeDuration)
        {
            // Increment fade time
            currentFadeTime += Time.deltaTime;

            // Calculate the interpolation factor for fading
            float fadeT = Mathf.Clamp01(currentFadeTime / fadeDuration);

            // Interpolate the volume from initialVolume to targetVolume over time
            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, fadeT);

            yield return null;
        }
    }
}
