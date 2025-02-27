using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Tooltip("AudioSource component playing the background music.")]
    public AudioSource audioSource;

    [Header("Timing (in seconds)")]
    [Tooltip("Time (in seconds) at which the music should start (e.g., 49 seconds for 0:49).")]
    public float startTime = 49f;
    [Tooltip("Time (in seconds) where the loop should start (e.g., 80 seconds for 1:20).")]
    public float loopStartTime = 80f;
    [Tooltip("Time (in seconds) where the loop should end (e.g., 155 seconds for 2:35).")]
    public float loopEndTime = 155f;

    void Start()
    {
        // If no AudioSource was assigned, try to get one from this GameObject.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            // Set the initial playback time and start playing.
            audioSource.time = startTime;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("BackgroundMusic: No AudioSource found!");
        }
    }

    void Update()
    {
        // If the music is playing and we've reached or passed the loop end time...
        if (audioSource != null && audioSource.isPlaying)
        {
            if (audioSource.time >= loopEndTime)
            {
                // Jump back to the loop start time.
                audioSource.time = loopStartTime;
            }
        }
    }
}
