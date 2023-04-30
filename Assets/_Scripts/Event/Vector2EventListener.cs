using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the Vector2Event scriptable object.
/// When the event raised, it can call the methods assigned to it with a Vector2 parameter.
/// </summary>
public class Vector2EventListener : MonoBehaviour
{
    #region Fields
    public Vector2Event gameEvent;
    public UnityEvent<UnityEngine.Vector2> response;
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
    public void OnEventRaised( UnityEngine.Vector2 param )
    {
        response.Invoke( param );
    }
    #endregion
}
