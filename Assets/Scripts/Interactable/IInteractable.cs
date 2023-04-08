namespace Interactable
{
    /// <summary>
    /// Interface for all interactable objects.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Makes the player interact with the object.
        /// </summary>
        void Interact();

        /// <summary>
        /// Gets the message that is shown to the player when they look at the interactable object.
        /// </summary>
        /// <returns>Shown message.</returns>
        string GetPromptMessage();
    }
}