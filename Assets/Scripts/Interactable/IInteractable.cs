namespace Interactable
{
    /// <summary>
    /// Interface for all interactable objects.
    /// </summary>
    public interface IInteractable
    {
        void Interact();

        string GetPromptMessage();
    }
}