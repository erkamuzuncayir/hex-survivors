using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HighScoreHolder : MonoBehaviour
{
#region Field
    public TextMeshProUGUI  highScore_text;
    public SharedInt    highScore;
    [FormerlySerializedAs("total_score_SO")] public SharedInt    totalScoreScriptableObject;
#endregion

#region Unity API
    void Start()
    {
        highScore_text.text = $"High Score: { highScore.value }";
    }
#endregion
    
#region Implementation
    public void SetHighScore()
    {
        if( highScore.value < totalScoreScriptableObject.value )
            highScore.value = totalScoreScriptableObject.value;

        highScore_text.text = $"High Score: { highScore.value }";
    }
#endregion
}
