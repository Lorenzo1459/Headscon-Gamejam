using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SFXManager instance;

    [SerializeField] private AudioSource SFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float destroyTime = audioSource.clip.length + 0.1f; // Add a small buffer to ensure the sound finishes playing
        Destroy(audioSource.gameObject, destroyTime);
    }

    public void PlayRandomSFX(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {   
        int randomIndex = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip[randomIndex];
        audioSource.volume = volume;
        audioSource.Play();
        float destroyTime = audioSource.clip.length + 0.1f; // Add a small buffer to ensure the sound finishes playing
        Destroy(audioSource.gameObject, destroyTime);
    }
}
