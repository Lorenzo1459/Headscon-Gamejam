using UnityEngine;


// For buttons or other triggers to play sound effects
public class PlaySFX : MonoBehaviour
{
    public AudioClip audioClip;
    
    public void PlaySound()
    {
        SFXManager.instance.PlaySFX(audioClip, transform, 1f);
    }
}
