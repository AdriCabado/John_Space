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

    private bool hasStarted = false;

    void Start()
    {
        // If no AudioSource was assigned, try to get one from this GameObject.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogWarning("BackgroundMusic: No AudioSource found!");
            return;
        }

        // Prevent music from starting immediately
        audioSource.Stop();
    }

    void Update()
    {
        // Start playing the music when Time.timeScale becomes 1 (game starts)
        if (!hasStarted && Time.timeScale == 1f)
        {
            hasStarted = true;
            audioSource.time = startTime;
            audioSource.Play();
        }

        // Looping logic
        if (hasStarted && audioSource.isPlaying && audioSource.time >= loopEndTime)
        {
            audioSource.time = loopStartTime;
        }
    }
}
