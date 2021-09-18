using UnityEngine;

namespace UI
{
    public class VisualsOrderInLayerAdjuster : MonoBehaviour
    {
        private SpriteRenderer _modelSpriteRenderer;

        private void Awake()
        {
            _modelSpriteRenderer = GetComponent<SpriteRenderer>();
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
                    _modelSpriteRenderer.sortingOrder += 5000;
                    break;
                case -1:
                    _modelSpriteRenderer.sortingOrder += 4000;
                    break;
                case 0:
                    _modelSpriteRenderer.sortingOrder += 3000;
                    break;
                case 1:
                    _modelSpriteRenderer.sortingOrder += 2000;
                    break;
                case 2:
                    _modelSpriteRenderer.sortingOrder += 1000;
                    break;
                default:
                    break;
            }
        }
    }
}
