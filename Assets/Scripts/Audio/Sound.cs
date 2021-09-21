using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public AudioClip Clip { get; private set; }
        
        [field:Range(0f, 1f)]
        [field:SerializeField] public float Volume { get; private set; } = 0.5f;
        
        [field:Range(.1f, 3f)]
        [field:SerializeField] public float Pitch { get; private set; } = 0.5f;
        [field:SerializeField] public bool Loop { get; private set; } = false;

        public AudioSource Source { get; set; }
    }
}