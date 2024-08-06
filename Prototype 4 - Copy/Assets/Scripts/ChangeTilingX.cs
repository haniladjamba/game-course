using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTilingX : MonoBehaviour
{
    public float speed = 1.0f;
    public float minTilingX = 1.0f;
    public float maxTilingX = 30.0f;

    private Material skyMaterial;
    private bool increasing = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get Material of sky dome
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            skyMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skyMaterial != null)
        {
            // calculate new tiling X value
            float newTilingX = Mathf.PingPong(Time.time * speed, 1.0f) * (maxTilingX - minTilingX) + minTilingX;

            // set the tiling X material value
            skyMaterial.mainTextureScale = new Vector2(newTilingX, skyMaterial.mainTextureScale.y);
        }
    }
}
