using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public Transform player;
    public Renderer groundRenderer;
    public Material hijabMaterial;
    public Color slumColor = new Color(0.3f, 0.3f, 0.3f);
    public Color richColor = new Color(0.5f, 0.9f, 0.8f);
    public Color hijabStartColor = new Color(0.2f, 0.2f, 0.2f);
    public Color hijabEndColor = new Color(0.8f, 0.8f, 0.8f);
    public float maxDistance = 1000f;

    void Start()
    {
        // assign player if not set
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
        // assign groundRenderer if not set
        if (groundRenderer == null)
        {
            GameObject ground = GameObject.Find("Ground");
            if (ground != null)
            {
                groundRenderer = ground.GetComponent<Renderer>();
            }
        }
        // assign hijabMaterial if not set
        if (hijabMaterial == null)
        {
            GameObject hijab = GameObject.Find("Hijab");
            if (hijab != null)
            {
                var rend = hijab.GetComponent<Renderer>();
                if (rend != null)
                    hijabMaterial = rend.material;
            }
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float progress = Mathf.Clamp01(player.position.z / maxDistance);
        if (groundRenderer != null)
        {
            Color newColor = Color.Lerp(slumColor, richColor, progress);
            groundRenderer.material.color = newColor;
        }
        if (hijabMaterial != null)
        {
            Color newHijabColor = Color.Lerp(hijabStartColor, hijabEndColor, progress);
            hijabMaterial.color = newHijabColor;
        }
    }
}
