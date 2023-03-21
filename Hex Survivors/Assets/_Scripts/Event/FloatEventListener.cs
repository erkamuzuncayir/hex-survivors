using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the FloatEvent scriptable object.
/// When the event raised, it can call the methods assigned to it with a float parameter.
/// </summary>
public class FloatEventListener : MonoBehaviour
{
    #region Fields
    public FloatEvent gameEvent;
    public UnityEvent<float> response;
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
    public void OnEventRaised( float param )
    {
        response.Invoke( param );
    }
    #endregion
}
