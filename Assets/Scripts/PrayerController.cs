using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrayerController : MonoBehaviour
{
    // Define prayer effects and variables here
    public float initialPrayerCooldown = 10f; // Initial cooldown duration in seconds
    private float prayerCooldown = 0f; // Current cooldown duration
    private float lastPrayerTime = -Mathf.Infinity; // Initialize last prayer time to negative infinity
    private bool isPrayerOnCooldown = false; // Flag to indicate if prayer is on cooldown

    // AnimatedText component for animating the prayer dialogue
    public AnimatedText animatedText;

    // Custom TextMeshProUGUI to display cooldown status
    public TextMeshProUGUI prayerTextMesh;

    // Define a class to hold the data for each prayer
    [System.Serializable]
    public class Prayer
    {
        public string prayerText;
        public string[] dialogueStrings;
    }

    // Array of custom prayers
    public Prayer[] customPrayers;

    void Start()
    {
        prayerCooldown = initialPrayerCooldown; // Set initial cooldown
        DeactivatePrayerText(); // Deactivate prayer text at start
    }

    void Update()
    {
        // Update cooldown timer
        if (isPrayerOnCooldown)
        {
            prayerCooldown -= Time.deltaTime; // Reduce cooldown timer

            // Check if cooldown has ended
            if (prayerCooldown <= 0f)
            {
                isPrayerOnCooldown = false;
                prayerCooldown = 0f;
                Debug.Log("Prayer cooldown ended. You can pray again.");
            }
        }

        // Check if the player pressed space to pray and prayer is not on cooldown
        if (Input.GetKeyDown(KeyCode.Space) && !isPrayerOnCooldown)
        {
            // Initiate prayer
            StartPrayer();
        }
        else if (isPrayerOnCooldown)
        {
            Debug.Log("Prayer is on cooldown. Please wait.");
            // Optionally provide feedback that prayer is on cooldown
        }
    }

    void StartPrayer()
    {
        // Get a random custom prayer
        if (customPrayers.Length > 0)
        {
            // Select a random custom prayer
            int randomIndex = Random.Range(0, customPrayers.Length);
            Prayer selectedPrayer = customPrayers[randomIndex];

            // Apply prayer effects here
            Debug.Log("Prayer initiated: " + selectedPrayer.prayerText);

            // Select a random dialogue string from the selected prayer
            string randomDialogue = selectedPrayer.dialogueStrings[Random.Range(0, selectedPrayer.dialogueStrings.Length)];
            Debug.Log("Prayer Dialogue: " + randomDialogue);

            // Activate the prayer text
            ActivatePrayerText();

            // Animate the prayer dialogue
            StartCoroutine(AnimatePrayerDialogue(randomDialogue));

            // Update last prayer time for cooldown
            lastPrayerTime = Time.time;

            // Set the prayer on cooldown
            isPrayerOnCooldown = true;
            prayerCooldown = 5f; // Set the cooldown duration (5 seconds in this case)
            Debug.Log("Prayer cooldown started. Please wait 5 seconds before praying again.");

            // Start coroutine to deactivate prayer text after a delay
            StartCoroutine(DeactivatePrayerTextDelayed(prayerCooldown));
        }
        else
        {
            Debug.LogError("No custom prayers defined.");
        }
    }

    void ActivatePrayerText()
    {
        prayerTextMesh.gameObject.SetActive(true);
    }

    void DeactivatePrayerText()
    {
        prayerTextMesh.gameObject.SetActive(false);
    }

    IEnumerator AnimatePrayerDialogue(string dialogue)
    {
        // Ensure the AnimatedText component exists
        if (animatedText != null)
        {
            // Display the prayer dialogue using the AnimatedText component
            yield return StartCoroutine(animatedText.ShowText());
        }
        else
        {
            Debug.LogError("AnimatedText component not found.");
        }
    }

    IEnumerator DeactivatePrayerTextDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        DeactivatePrayerText();
    }


}
