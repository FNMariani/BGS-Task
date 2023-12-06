using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInteract : InteractableObject
{
    public GameObject message;

    public override void Interact()
    {

    }
    public override void ShowInteract()
    {
        message.SetActive(true);
    }
}
