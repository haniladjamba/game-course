using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFogColor : MonoBehaviour
{
    public float speed = 1.0f;
    public float minColorValue = 30.0f;
    public float maxColorValue = 200.0f;

    private ParticleSystem fogParticleSystem;
    private ParticleSystem.MainModule mainModule;

    // Start is called before the first frame update
    void Start()
    {
        // Get the ParticleSystem component
        fogParticleSystem = GetComponent<ParticleSystem>();

        // Check if particle system is assigned
        if (fogParticleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if fogParticleSystem and mainModule are assigned
        if (fogParticleSystem != null)
        {
            // Get main module of particle system
            ParticleSystem.MainModule mainModule = fogParticleSystem.main;

            // Calculate the new green (G) and blue (B) value
            float newG = Mathf.PingPong(Time.time * speed, 1.0f) * (maxColorValue - minColorValue) + minColorValue;
            float newB = Mathf.PingPong(Time.time * speed + 0.5f, 1.0f) * (maxColorValue - minColorValue) + minColorValue;

            // Get the current start color
            Color currentColor = mainModule.startColor.color;

            // set the new G and B values while keeping the original R and A values
            Color newColor = new Color(currentColor.r, newG / 255.0f, newB / 255.0f, currentColor.a);

            // Set the new start color
            mainModule.startColor = newColor;
        }
    }
}
