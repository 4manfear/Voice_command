using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windeffect : MonoBehaviour
{
    public ParticleSystem windParticleSystem;   // The particle system for wind
    public Vector3 windDirection = Vector3.right; // Default direction
    public float windStrength = 5f;             // Wind speed
    public float particleEmissionRate = 50f;    // How many particles are emitted per second

    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    void Start()
    {
        // Get the modules of the particle system
        emissionModule = windParticleSystem.emission;
        mainModule = windParticleSystem.main;
        velocityModule = windParticleSystem.velocityOverLifetime;

        // Set the initial wind settings
        ConfigureParticleSystem();
    }

    void ConfigureParticleSystem()
    {
        // Set particle emission rate
        emissionModule.rateOverTime = particleEmissionRate;

        // Set wind direction and strength
        velocityModule.x = windDirection.x * windStrength;
        velocityModule.y = windDirection.y * windStrength;
        velocityModule.z = windDirection.z * windStrength;
    }

    // Change wind direction in real-time
    public void SetWindDirection(Vector3 newDirection)
    {
        windDirection = newDirection;
        velocityModule.x = windDirection.x * windStrength;
        velocityModule.y = windDirection.y * windStrength;
        velocityModule.z = windDirection.z * windStrength;
    }

    // Change wind strength in real-time
    public void SetWindStrength(float newStrength)
    {
        windStrength = newStrength;
        mainModule.startSpeed = windStrength;
    }
}
