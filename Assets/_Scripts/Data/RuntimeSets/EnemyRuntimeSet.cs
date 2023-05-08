using _Scripts.Actors;
using UnityEngine;

namespace _Scripts.Data.RuntimeSets
{
    [CreateAssetMenu(fileName = "EnemyRuntimeSet", menuName = "RuntimeSet/Enemy Script")]
    public class EnemyRuntimeSet : RuntimeSet<Enemy>
    {
    }
}