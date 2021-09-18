using UnityEngine;

namespace UI
{
    public class Shadow : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sortingOrder = GetComponentsInParent<SpriteRenderer>()[1].sortingOrder - 1;
        }
    }
}
