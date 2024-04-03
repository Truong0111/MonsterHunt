using UnityEngine;

/// <summary>
/// Set ParticleSystem StopAction = callback to use.
/// </summary>
public class AutoDestructorFX : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        SimplePool.Despawn(gameObject);
    }
}