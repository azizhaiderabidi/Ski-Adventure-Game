using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.5f, 0f); // Adjust speed from Inspector

    private Renderer objRenderer;
    private Vector2 currentOffset;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        currentOffset = objRenderer.material.mainTextureOffset;
    }

    void Update()
    {
        // Move texture based on time
        currentOffset += scrollSpeed * Time.deltaTime;
        objRenderer.material.mainTextureOffset = currentOffset;
    }
}
