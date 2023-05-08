using System.Collections;
using System.Collections.Generic;
using _Scripts.Systems;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerTurnStateSO 
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private PathfindingSystemSO _pathfindingSystem;
    
    //public void Move();
    /*
    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public override void CaptureInput()
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeState()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        // _animatorController.SetIdle();
    }
    
    */
}
