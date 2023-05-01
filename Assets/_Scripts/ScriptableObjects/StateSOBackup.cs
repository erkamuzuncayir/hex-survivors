using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateSOBackup : ScriptableObject
{
    public abstract void Init();

    public abstract void CaptureInput();

    public abstract void ChangeState();

    public abstract void Exit();
}
