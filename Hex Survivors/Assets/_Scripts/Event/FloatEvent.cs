using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// This class inherits scriptable object to create customized event layer for project.
/// </summary>
[CreateAssetMenu(fileName = "Float Event", menuName = "Events/Float Event")]
public class FloatEvent : DescriptionBaseSO
{
    #region Fields
    List< FloatEventListener > _eventListenerList = new();
    public bool hasAnyDependent;
    [ ShowIf( "hasAnyDependent" ) ]
    public List< FloatEvent > eventsDependOnThis;
    #endregion
    
    #region API
    [Button()]
    public void Raise( float param )
    {
        for ( int i = _eventListenerList.Count - 1; i >= 0; i-- )
            _eventListenerList[ i ].OnEventRaised( param );
    }
    [ ShowIf( "hasAnyDependent" ), Button( "Raise Self and Dependents" ) ]
    public void RaiseSelfAndDependents( float param )
    {
        Raise( param );

        if( hasAnyDependent && eventsDependOnThis != null )
            for( var i = 0; i < eventsDependOnThis.Count; i++ )
                eventsDependOnThis[ i ].Raise( param );
    }
    public void RegisterListener( FloatEventListener listener )
    { 
        _eventListenerList.Add( listener ); 
    }

    public void UnregisterListener( FloatEventListener listener )
    { 
        _eventListenerList.Remove( listener ); 
    }
    #endregion

    #region Editor Only
#if UNITY_EDITOR
#endif
    #endregion
}
