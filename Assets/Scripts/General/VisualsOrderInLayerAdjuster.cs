using UnityEngine;

namespace General
{
    public static class VisualsOrderInLayerAdjuster
    {
        public static void SetYSortingOrder(SpriteRenderer spriteRenderer, float yPosition)
        {
            switch (Mathf.RoundToInt(yPosition))
            {
                case -2:
                    spriteRenderer.sortingOrder += 5000;
                    break;
                case -1:
                    spriteRenderer.sortingOrder += 4000;
                    break;
                case 0:
                    spriteRenderer.sortingOrder += 3000;
                    break;
                case 1:
                    spriteRenderer.sortingOrder += 2000;
                    break;
                case 2:
                    spriteRenderer.sortingOrder += 1000;
                    break;
                default:
                    break;
            }
        }
    }
}
