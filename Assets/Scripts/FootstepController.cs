using UnityEngine;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(AudioSource))]
public class FootstepController : MonoBehaviour
{
    [System.Serializable]
    public class MaterialFootstepSounds
    {
        public Material[] materials;
        public AudioClip[] footstepSounds;
    }

    public MaterialFootstepSounds[] materialFootstepSounds; // Array of footstep sound clips for different materials
    public float walkingFootstepInterval = 0.5f; // Time interval between footstep sounds when walking
    public float runningFootstepInterval = 0.3f; // Time interval between footstep sounds when running

    private AudioSource audioSource;
    private float currentFootstepInterval;
    private float nextFootstepTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentFootstepInterval = walkingFootstepInterval;
        nextFootstepTime = 0f;
    }

    void Update()
    {
        // Update footstep interval based on the player's movement
        currentFootstepInterval = walkingFootstepInterval;

        // Play footstep sounds at intervals when the player is moving on the ground
        if (IsPlayerMoving() && Time.time >= nextFootstepTime)
        {
            PlayFootstepSound();
            nextFootstepTime = Time.time + currentFootstepInterval;
        }
    }

    void PlayFootstepSound()
    {
        // Perform a raycast downwards from the player's position to detect the ground or surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Check if the object has a ProBuilder mesh
            ProBuilderMesh pbMesh = hit.collider.GetComponent<ProBuilderMesh>();
            if (pbMesh != null)
            {
                // Get all sub-meshes from the ProBuilder mesh
                ProBuilderMesh[] subMeshes = pbMesh.GetComponents<ProBuilderMesh>();

                // Iterate through each sub-mesh
                foreach (ProBuilderMesh subMesh in subMeshes)
                {
                    // Get all materials from the sub-mesh
                    Material[] materials = subMesh.GetComponent<Renderer>().sharedMaterials;

                    // Iterate through each material of the sub-mesh
                    foreach (Material material in materials)
                    {
                        // Find the footstep sounds for the current material
                        foreach (var materialFootstepSound in materialFootstepSounds)
                        {
                            if (ArrayContainsMaterial(materialFootstepSound.materials, material))
                            {
                                // Play a random footstep sound for the current material
                                AudioClip[] footstepSounds = materialFootstepSound.footstepSounds;
                                if (footstepSounds != null && footstepSounds.Length > 0)
                                {
                                    int randomIndex = Random.Range(0, footstepSounds.Length);
                                    audioSource.clip = footstepSounds[randomIndex];
                                    audioSource.Play();
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
    bool IsPlayerMoving()
    {
        // Check if the player is moving using input axes
        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f;
    }

    bool ArrayContainsMaterial(Material[] array, Material material)
    {
        foreach (Material mat in array)
        {
            if (mat == material)
            {
                return true;
            }
        }
        return false;
    }
}