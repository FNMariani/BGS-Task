using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionSystem : MonoBehaviour
{
    public float interactionRange = 2f;
    public KeyCode interactKey = KeyCode.E;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }

        //DEBUG
        Vector2 raycastDirection = transform.right;
        Vector2 raycastOrigin = (Vector2)transform.position + raycastDirection * 0.5f;
        Debug.DrawRay(raycastOrigin, raycastDirection * interactionRange, Color.red);
    }

    void TryInteract()
    {
        Vector2 raycastDirection = transform.right;
        Vector2 raycastOrigin = (Vector2)transform.position + raycastDirection * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, interactionRange);

        if (hit.collider != null)
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

            if (interactableObject != null)
            {
                interactableObject.Interact();
            }
        }
    }
}
