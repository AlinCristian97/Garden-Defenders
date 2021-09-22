using UnityEngine;

namespace General
{
    public static class ShadowOrderInLayerAdjuster
    {
        public static void SetShadowSortingOrder(SpriteRenderer spriteRenderer, SpriteRenderer parentSpriteRenderer)
        {
            spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;
        }
    }
}