using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Game State", fileName = "GameState")]
public class StateSO : ScriptableObject
{
    public enum State
    {
        MainMenu,
        PlayerTurn,
        EnemyTurn
    }

    public State GameState = State.PlayerTurn;
}
