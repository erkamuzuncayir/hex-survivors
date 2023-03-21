using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// This class inherits scriptable object to create customized event layer for project.
/// </summary>
[CreateAssetMenu(fileName = "Bool Event", menuName = "Events/Bool Event")]
public class BoolEvent : DescriptionBaseSO
{
    #region Fields
    List< BoolEventListener > _eventListenerList = new();
    public bool hasAnyDependent;
    [ ShowIf( "hasAnyDependent" ) ]
    public List< VoidEvent > eventsDependOnThis;
    #endregion
    
    #region API
    [Button()]
    public void Raise( bool param )
    {
        for ( int i = _eventListenerList.Count - 1; i >= 0; i-- )
            _eventListenerList[ i ].OnEventRaised( param );
    }
    [ ShowIf( "hasAnyDependent" ), Button( "Raise Self and Dependents" ) ]
    public void RaiseSelfAndDependents( bool param )
    {
        Raise( param );

        if( hasAnyDependent && eventsDependOnThis != null )
            for( var i = 0; i < eventsDependOnThis.Count; i++ )
                eventsDependOnThis[ i ].Raise();
    }
    public void RegisterListener( BoolEventListener listener )
    { 
        _eventListenerList.Add( listener ); 
    }

    public void UnregisterListener( BoolEventListener listener )
    { 
        _eventListenerList.Remove( listener ); 
    }
    #endregion

    #region Editor Only
#if UNITY_EDITOR
#endif
    #endregion
}
