using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPickup : MonoBehaviour
{
    public SpawnManager spawnManager;
    public Material tempMaterialSky;
    public Material tempMaterialGround;
    public Enemy enemyScript;
    public Color newFogColor = Color.red;
    public float materialChangeDuration = 5.0f;
    public float spawnRange = 9.0f;

    public Material groundOriginalMaterial;

    private void Start()
    {
        StartCoroutine(WaitAndAccessEnemy());
    }

    IEnumerator WaitAndAccessEnemy()
    {
        // Wait for 3 second
        yield return new WaitForSeconds(1);

        // Now you can access the spawned enemy
        GameObject enemy = spawnManager.spawnedEnemies[0];
        if (enemy != null)
        {
            Debug.Log("enemy found");

            // Get enemy script
            enemyScript = enemy.GetComponent<Enemy>();
        }
        else
        {
            Debug.Log("enemy not found!!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply temporary materials
            ApplyTemporaryMaterials();

            if (enemyScript != null)
            {
                enemyScript.ApplyBuff();
                Debug.Log("isPlayerBuffed:" + enemyScript.isPlayerBuffed);
            } else
            {
               Debug.Log("Failed to applied buff to player!!");
            }

            // Start a timer to revert materials after materialChangeDuration
            Invoke("RevertMaterials", materialChangeDuration);

            MoveToRandomLocation();
        }
    }

    private void ApplyTemporaryMaterials()
    {
       
        // change the fog particle color
        GameObject fogObject = GameObject.Find("FX_Mist");
        if (fogObject != null)
        {
            ParticleSystem fogParticleSystem = fogObject.GetComponent<ParticleSystem>();
            if (fogParticleSystem != null)
            {
                ParticleSystem.MainModule mainModule = fogParticleSystem.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(newFogColor);
            }
        }

        // Apply temporary material to the ground object
        GameObject groundObject = GameObject.Find("Island");
        if (groundObject != null)
        {
            Renderer groundRenderer = groundObject.GetComponent<Renderer>();
            if (groundRenderer != null && tempMaterialGround != null)
            {
                groundRenderer.material = tempMaterialGround;
            }
        }
    }

    private void RevertMaterials()
    {
       

        // revert color of fog particle
        GameObject fogObject = GameObject.Find("FX_Mist");
        if (fogObject != null)
        {
            ParticleSystem fogParticleSystem = fogObject.GetComponent<ParticleSystem>();
            if (fogParticleSystem != null)
            {
                ParticleSystem.MainModule mainModule = fogParticleSystem.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(Color.white);
            }
        }

        // Revert material of the ground object
        GameObject groundObject = GameObject.Find("Island");
        if (groundObject != null)
        {
            Renderer groundRenderer = groundObject.GetComponent<Renderer>();
            if (groundRenderer != null)
            {
                groundRenderer.material = groundOriginalMaterial;
            }
        }
    }

    private void MoveToRandomLocation()
    {
        // Generate a random new location within the spawn range
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRange, spawnRange), 0f, Random.Range(-spawnRange, spawnRange));
        Vector3 newLocation = transform.position + randomOffset;

        // Move the super_pickup to the new random location
        transform.position = newLocation;
    }
}

