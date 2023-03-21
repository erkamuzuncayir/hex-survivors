using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the IntEvent scriptable object.
/// When the event raised, it can call the methods assigned to it with a int parameter.
/// </summary>
public class IntEventListener : MonoBehaviour
{
    #region Fields
    public IntEvent gameEvent;
    public UnityEvent<int> response;
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
    public void OnEventRaised( int param )
    {
        response.Invoke( param );
    }
    #endregion
}
