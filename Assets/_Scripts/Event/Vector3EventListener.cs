using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the Vector3Event scriptable object.
/// When the event raised, it can call the methods assigned to it with a Vector3 parameter.
/// </summary>
public class Vector3EventListener : MonoBehaviour
{
    #region Fields
    public Vector3Event gameEvent;
    public UnityEvent<UnityEngine.Vector3> response;
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
    public void OnEventRaised( UnityEngine.Vector3 param )
    {
        response.Invoke( param );
    }
    #endregion
}
