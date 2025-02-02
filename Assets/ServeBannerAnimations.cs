using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serveBannerAnimations : MonoBehaviour
{
    private Vector3 objectSize;
    private int prevScreenWidth;
    private int prevScreenHeight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedResize());
    }   

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != prevScreenWidth || Screen.height != prevScreenHeight) resizeToScreen();
        prevScreenWidth = Screen.width;
        prevScreenHeight = Screen.height;
    }
    IEnumerator DelayedResize()
    {
        yield return new WaitForEndOfFrame();
        resizeToScreen();
    }
    void resizeToScreen()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer not found!");
            return;
        }

        // Get the correct Z depth for conversion
        float spriteZ = transform.position.z - Camera.main.transform.position.z;

        // Get world width of the screen
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, spriteZ));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, spriteZ));
        float screenWidthWorldUnits = Mathf.Abs(rightEdge.x - leftEdge.x);

        // Target width is 80% of the screen width
        float targetWidth = screenWidthWorldUnits * 0.8f;

        // Get the sprite’s **original width and height**
        float originalSpriteWidth = sr.sprite.bounds.size.x; 
        float originalSpriteHeight = sr.sprite.bounds.size.y;
        float currentScaleX = transform.localScale.x;
        float currentScaleY = transform.localScale.y;

        // Correct scale factor based on the original width
        float scaleFactor = targetWidth / (originalSpriteWidth * currentScaleX);

        // Apply uniform scale to keep aspect ratio
        transform.localScale = new Vector3(scaleFactor * currentScaleX, scaleFactor * currentScaleY, 1);

        Debug.Log($"Resized {gameObject.name}: targetWidth={targetWidth}, originalWidth={originalSpriteWidth}, finalScaleX={transform.localScale.x}, finalScaleY={transform.localScale.y}");
    }
}
