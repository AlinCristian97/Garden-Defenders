using UnityEngine;

namespace UI
{
    public class VisualsOrderInLayerAdjuster : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetYSortingOrder();
        }

        private void SetYSortingOrder()
        {
            switch (Mathf.RoundToInt(transform.position.y))
            {
                case -2:
                    _spriteRenderer.sortingOrder += 5000;
                    break;
                case -1:
                    _spriteRenderer.sortingOrder += 4000;
                    break;
                case 0:
                    _spriteRenderer.sortingOrder += 3000;
                    break;
                case 1:
                    _spriteRenderer.sortingOrder += 2000;
                    break;
                case 2:
                    _spriteRenderer.sortingOrder += 1000;
                    break;
                default:
                    break;
            }
        }
    }
}
