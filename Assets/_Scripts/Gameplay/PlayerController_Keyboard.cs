using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_Keyboard : MonoBehaviour
{
#region Fields
    public FloatReference   speed;
    Rigidbody               player_rigidbody;
#endregion

#region Unity API
    void Start()
    {
        player_rigidbody = GetComponent< Rigidbody >();
    }

    void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis( "Horizontal" );
        var moveVertical = Input.GetAxis( "Vertical" );
        
        Vector3 movement = new( moveHorizontal, 0.0f , moveVertical );

        player_rigidbody.AddForce( movement * speed.Value );
    }
#endregion

#region Implementations
    public void DeactivateItself()
    {
        enabled = false;
    }
#endregion
}
