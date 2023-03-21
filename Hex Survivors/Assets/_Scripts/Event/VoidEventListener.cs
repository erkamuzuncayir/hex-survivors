using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the VoidEvent scriptable object.
/// When the event raised, it can call the methods assigned to it.
/// </summary>
public class VoidEventListener : MonoBehaviour
{
    #region Fields
    public VoidEvent gameEvent;
    public UnityEvent response;
    #endregion

    #region Unity API
    void OnEnable()
    {
        gameEvent.RegisterListener( this );
    }

    void OnDisable()
    {
        gameEvent.UnregisterListener( this );
    }
    #endregion

    #region API
    public void OnEventRaised()
    {
        response.Invoke();
    }
    #endregion
}
