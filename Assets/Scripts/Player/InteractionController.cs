using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    static Color activeColor = new Color(1f, 1f, 1f, 1f);
    static Color inactiveColor = new Color(1f, 1f, 1f, 0.7f);
    
    public Interactable[] interactables;
    public GameObject interactionBubble;
    public SpriteRenderer iconRenderer;
    public bool interactionActive = false;

    private SpriteRenderer bubbleSpriteRenderer;
    private Interactable current;
    private int selectionIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.interactionBubble.SetActive(false);
        this.bubbleSpriteRenderer = this.interactionBubble.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
    }
    
    // Handle key interface
    void handleKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.interactionActive = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.selectionIndex--;
            if (this.selectionIndex < 0)
                this.selectionIndex = this.current.interactions.Length - 1;
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.selectionIndex++;
            if (this.selectionIndex >= this.current.interactions.Length)
                this.selectionIndex = 0;
        }
    }

    public void activate()
    {
        this.interactionActive = true;
        this.selectionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = this.transform.position;
        Interactable closest = null;
        float closestDist = 0.5f; // Also serves as threshold
        foreach (Interactable interactable in this.interactables)
        {
            var dist = Mathf.Abs(interactable.transform.position.x - playerPosition.x);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = interactable;
            }
        }
        
        // If we've identified a nearby interactable, enable it
        if (closest)
        {
            var pos = interactionBubble.transform.position;
            pos.x = closest.transform.position.x;
            interactionBubble.transform.position = pos;
            if (closest.interactions.Length > 0)
            {
                var defaultInteraction = closest.interactions[this.selectionIndex];
                iconRenderer.sprite = defaultInteraction.sprite;
                interactionBubble.SetActive(true);
            }

            if (this.interactionActive)
            {
                this.handleKeyInput();
            }

            this.current = closest;
        }
        else
        {
            interactionBubble.SetActive(false);
            this.interactionActive = false;
        }
        
        // Set alpha based on interaction engagement
        var alphaColor = this.interactionActive
            ? InteractionController.activeColor
            : InteractionController.inactiveColor;
        this.iconRenderer.color = alphaColor;
        this.bubbleSpriteRenderer.color = alphaColor;
    }
}
