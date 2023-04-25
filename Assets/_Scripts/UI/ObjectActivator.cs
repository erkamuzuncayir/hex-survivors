using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
#region Fields 
    public List< AffectedObject > affected_object_list= new();
#endregion

#region Unity API
    private void Start()
    {
        foreach ( var condition in affected_object_list )
        {
            if ( condition.activeInStart )
                ActivateObjectInStart();
            if( condition.deactiveInStart )
                DeactivateObjectInStart();
        }
    }
#endregion

#region Implementation
    public void ActivateObjectInStart()
    {
        foreach ( var condition in affected_object_list )
        {
            if( condition.activeInStart )
                condition.affected_gameObject.SetActive( true );
        }
    }

    public void DeactivateObjectInStart()
    {
        foreach ( var condition in affected_object_list )
        {
            if ( condition.deactiveInStart )
                condition.affected_gameObject.SetActive( false );
        }
    }

    public void ActivateObject (bool isPlayerWon ) 
    {
        for ( int i = 0; i < affected_object_list.Count; i++ )
        {
            if ( isPlayerWon && affected_object_list[ i ].is_winState == true )
                affected_object_list[ i ].affected_gameObject.SetActive( true );

            else if ( !isPlayerWon && affected_object_list[ i ].is_loseState == true )
                affected_object_list[ i ].affected_gameObject.SetActive( true );
        }
    }

    public void DectivateObject( bool isPlayerWon ) // false for lose condition
    {
        for ( int i = 0; i < affected_object_list.Count; i++ )
        {
            if ( isPlayerWon && affected_object_list[ i ].is_winState == false )
                affected_object_list[ i ].affected_gameObject.SetActive( false );
            else if ( !isPlayerWon && affected_object_list[i].is_loseState == false )
                affected_object_list[ i ].affected_gameObject.SetActive( false );
        }
    }
}

// Info : This class provides us a good inspector experience with editing and debugging.
[System.Serializable]
public class AffectedObject
{
    public GameObject   affected_gameObject;
    public string       state_tag;
    public bool         activeInStart;
    public bool         deactiveInStart;
    public bool         is_winState;
    public bool         is_loseState;
#endregion
}
