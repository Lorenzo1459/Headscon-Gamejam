using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioClip clip;

    public void PlaySFX()
    {
        if (clip != null)
        {
            SFXManager.instance.PlaySFX(clip, transform, 1.0f);
        }
        else
        {
            Debug.LogWarning("Audio clip is not assigned.");
        }
    }
}
