using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// This class inherits scriptable object to create customized event layer for project.
/// </summary>
[CreateAssetMenu(fileName = "Vector3 Event", menuName = "Events/Vector3 Event")]
public class Vector3Event : DescriptionBaseSO
{
    #region Fields
    List< Vector3EventListener > _eventListenerList = new();
    public bool hasAnyDependent;
    [ ShowIf( "hasAnyDependent" ) ]
    public List< Vector3Event > eventsDependOnThis;
    #endregion
    
    #region API
    [Button()]
    public void Raise( Vector3 param )
    {
        for ( int i = _eventListenerList.Count - 1; i >= 0; i-- )
            _eventListenerList[ i ].OnEventRaised( param );
    }
    [ ShowIf( "hasAnyDependent" ), Button( "Raise Self and Dependents" ) ]
    public void RaiseSelfAndDependents( Vector3 param )
    {
        Raise( param );

        if( hasAnyDependent && eventsDependOnThis != null )
            for( var i = 0; i < eventsDependOnThis.Count; i++ )
                eventsDependOnThis[ i ].Raise( param );
    }
    public void RegisterListener( Vector3EventListener listener )
    { 
        _eventListenerList.Add( listener ); 
    }

    public void UnregisterListener( Vector3EventListener listener )
    { 
        _eventListenerList.Remove( listener ); 
    }
    #endregion

    #region Editor Only
#if UNITY_EDITOR
#endif
    #endregion
}