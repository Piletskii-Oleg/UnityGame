using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}
