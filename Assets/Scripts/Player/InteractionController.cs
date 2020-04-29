using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public Interactable[] interactables;
    public GameObject interactionBubble;
    public SpriteRenderer iconRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.interactionBubble.SetActive(false);
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
                var defaultInteraction = closest.interactions[0];
                iconRenderer.sprite = defaultInteraction.sprite;
                interactionBubble.SetActive(true);
            }
        }
        else
        {
            interactionBubble.SetActive(false);
        }
    }
}
