using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Event to register with.")]
    private GameEvent gameEvent;

    [SerializeField]
    [Tooltip("Response to invoke when Game Event is raised.")]
    private UnityEvent response;

    public void OnEventRaised()
        => response.Invoke();

    private void OnEnable()
        => gameEvent.RegisterListener(this);

    private void OnDisable()
        => gameEvent.UnregisterListener(this);
}