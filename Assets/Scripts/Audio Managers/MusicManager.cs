using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Corrigido para static

    private AudioSource audioSource;
    public AudioClip[] musicClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Mantém o objeto entre as cenas
        }
        else
        {
            Destroy(gameObject); // Destroi instâncias duplicadas
            return;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    public void PlayRandomMusic()
    {
        if (musicClips.Length == 0)
        {
            Debug.LogWarning("No music clips assigned to MusicManager.");
            return;
        }
        int randomIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[randomIndex];
        audioSource.Play();
    }
}