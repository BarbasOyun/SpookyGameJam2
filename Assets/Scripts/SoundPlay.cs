using UnityEngine;
using UnityEngine.Audio;

public class SoundPlay : MonoBehaviour
{

    public AudioManager.SFX ObjectType;

    void OnCollisionEnter(Collision collision)
    {
        // Play a sound if the colliding objects had a big impact.
        if (collision.relativeVelocity.magnitude > 1)
            AudioManager.instance.PlaySfx(ObjectType);
    }
}
