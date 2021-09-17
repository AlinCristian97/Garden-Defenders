using UnityEngine;

namespace UI
{
    public class UnitVisualsOrderInLayerAdjuster : MonoBehaviour
    {
        private Unit _parent;

        private void Awake()
        {
            if (transform.parent != null)
            {
                _parent = GetComponentInParent<Unit>();
            }
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
                    _parent.SpriteRenderer.sortingOrder += 5000;
                    break;
                case -1:
                    _parent.SpriteRenderer.sortingOrder += 4000;
                    break;
                case 0:
                    _parent.SpriteRenderer.sortingOrder += 3000;
                    break;
                case 1:
                    _parent.SpriteRenderer.sortingOrder += 2000;
                    break;
                case 2:
                    _parent.SpriteRenderer.sortingOrder += 1000;
                    break;
                default:
                    break;
            }
        }
    }
}
