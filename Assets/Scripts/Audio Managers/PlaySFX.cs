using UnityEngine;


// For buttons or other triggers to play sound effects
public class PlaySFX : MonoBehaviour
{
    public AudioClip audioClip;

    public float volume = 1f; // Default volume, can be adjusted in the inspector   
    public void PlaySound()
    {
        SFXManager.instance.PlaySFX(audioClip, transform, volume);
    }
}
