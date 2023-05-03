using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class listens to the GameObjectEvent scriptable object.
/// When the event raised, it can call the methods assigned to it with a int parameter.
/// </summary>
public class GameObjectEventListener : MonoBehaviour
{
    #region Fields
    public GameObjectEvent gameEvent;
    public UnityEvent<GameObject> response;
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
    public void OnEventRaised( GameObject param )
    {
        response.Invoke( param );
    }
    #endregion
}
