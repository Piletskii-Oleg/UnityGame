using UnityEngine;

/// <summary>
/// Basic class for all interactable objects. Uses template method.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private string promptMessage;

    /// <summary>
    /// Basic interact method.
    /// </summary>
    public void BaseInteract()
    {
        Interact();
    }

    // is redefined in inheritor classes
    protected virtual void Interact()
    {
    }
}
