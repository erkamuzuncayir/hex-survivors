using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// This class inherits scriptable object to create customized event layer for project.
/// </summary>
[CreateAssetMenu(fileName = "Void Event", menuName = "Events/Void Event")]
public class VoidEvent : DescriptionBaseSO
{
    #region Fields
    List< VoidEventListener > _eventListenerList = new();
    public bool hasAnyDependent;
    [ ShowIf( "hasAnyDependent" ) ]
    public List< VoidEvent > eventsDependOnThis;
    #endregion
    
    #region API
    [Button()]
    public void Raise()
    {
        for ( int i = _eventListenerList.Count - 1; i >= 0; i-- )
            _eventListenerList[ i ].OnEventRaised();
    }
    [ ShowIf( "hasAnyDependent" ), Button( "Raise Self and Dependents" ) ]
    public void RaiseSelfAndDependents()
    {
        Raise();

        if( hasAnyDependent && eventsDependOnThis != null )
            for( var i = 0; i < eventsDependOnThis.Count; i++ )
                eventsDependOnThis[ i ].Raise();
    }
    public void RegisterListener( VoidEventListener listener )
    { 
        _eventListenerList.Add( listener ); 
    }

    public void UnregisterListener( VoidEventListener listener )
    { 
        _eventListenerList.Remove( listener ); 
    }
    #endregion

    #region Editor Only
#if UNITY_EDITOR
#endif
    #endregion
}