using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip stumbleSound;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}   
