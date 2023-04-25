using UnityEngine;

/// <summary>
/// Base class for ScriptableObjects that need a public description field.
/// </summary>
public class DescriptionBaseSO : ScriptableObject
{
    [Header ("You should consider writing a description.")]
    [TextArea] public string description;
}