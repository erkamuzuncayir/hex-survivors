using UnityEngine;

namespace _Scripts.Data.Type
{
    public class DataSO<TData> : ScriptableObject
    {
        public TData Value;
    }
}