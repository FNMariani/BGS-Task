using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionSystem : MonoBehaviour
{
    public float interactionRange = 0.5f;
    public KeyCode interactKey = KeyCode.E;

    private PlayerController playerControllerRef;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerControllerRef = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }

        //DEBUG
        Vector2 raycastDirection = GetDirectionFromPlayer();
        Vector2 raycastOrigin = (Vector2)transform.position + raycastDirection * 1.0f;
        Debug.DrawRay(raycastOrigin, raycastDirection * interactionRange, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, interactionRange);

        if (hit.collider != null)
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

            if (interactableObject != null)
            {
                interactableObject.ShowInteract();
            }
            else
            {
                CheckInteractables();
            }
        }
        else
        {
            CheckInteractables();
        }
    }

    void CheckInteractables()
    {
        GameObject[] emotes = GameObject.FindGameObjectsWithTag("Emote");

        foreach (GameObject emote in emotes)
        {
            emote.GetComponent<SpriteRenderer>().enabled = false;
        }

        GameObject[] signs = GameObject.FindGameObjectsWithTag("Sign");

        foreach (GameObject sign in signs)
        {
            sign.SetActive(false);
        }
    }

    void TryInteract()
    {
        Vector2 raycastDirection = GetDirectionFromPlayer();
        Vector2 raycastOrigin = (Vector2)transform.position + raycastDirection * 1.0f;
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

    Vector2 GetDirectionFromPlayer()
    {
        if (playerControllerRef.lastDirection == 1) return transform.right;
        if (playerControllerRef.lastDirection == 2) return -transform.right;
        if (playerControllerRef.lastDirection == 3) return transform.up;
        if (playerControllerRef.lastDirection == 4) return -transform.up;

        return Vector2.zero;
    }
}
