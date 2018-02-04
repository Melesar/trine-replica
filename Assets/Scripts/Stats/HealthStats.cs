using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Health stats")]
    public class HealthStats : ScriptableObject
    {
        public int initialHealth;
    }
}