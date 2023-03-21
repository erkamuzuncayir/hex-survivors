using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the BoolEvent scriptable object.
/// When the event raised, it can call the methods assigned to it with a boolean parameter.
/// </summary>
public class BoolEventListener : MonoBehaviour
{
    #region Fields
    public BoolEvent gameEvent;
    public UnityEvent<bool> response;
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
    public void OnEventRaised( bool param )
    {
        response.Invoke( param );
    }
    #endregion
}
