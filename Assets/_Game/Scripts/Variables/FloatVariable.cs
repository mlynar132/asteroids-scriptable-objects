using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Variables
{
    [CreateAssetMenu(fileName = "new FloatVariable", menuName = "ScriptableObjects/Variables/FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
        [Range(0f, 10f)]
        [SerializeField] private float _value;

        public float Value => _value;

        public void SetValue(float value)
        {
            _value = value;
        }
    }
}
