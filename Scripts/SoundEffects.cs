using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffects : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffects Instance;

    public AudioClip explosionSound;
    public AudioClip playerShotSound;
    public AudioClip enemyShotSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffects!");
        }
        Instance = this;
    }

    public void MakeExplosionSound()
    {
        MakeSound(explosionSound);
    }

    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }

    public void MakeEnemyShotSound()
    {
        MakeSound(enemyShotSound);
    }

    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
