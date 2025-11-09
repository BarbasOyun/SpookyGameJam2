using UnityEngine;
using UnityEngine.Audio;

public class SoundPlay : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Play a sound if the colliding objects had a big impact.
        if (collision.relativeVelocity.magnitude > 1)
            audioSource.Play();
    }
}
