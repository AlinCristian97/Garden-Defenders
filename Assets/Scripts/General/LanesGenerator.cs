using System;
using General.Patterns.Singleton;
using UnityEngine;

namespace General
{
    public class LanesGenerator : MonoBehaviour
    {
        #region Singleton

        private static LanesGenerator _instance;
        
        public static LanesGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LanesGenerator>();
                }
                
                return _instance;
            }
        }

        #endregion
    
        [Header("Buildable Grid Tiles")]
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _tileParent;
        
        private readonly Vector2 _gridOriginPosition = new Vector2(-3f, -2f);

        [Header("Lawn Mowers")] 
        [SerializeField] private bool _levelHasLawnMowers;
        [SerializeField] private LawnMower _lawnMowerPrefab;
        [SerializeField] private Transform _lawnMowersContainer;
    
        private const byte BUILDABLE_AREA_GRID_ROW_COUNT = 5;
        private const byte BUILDABLE_AREA_GRID_COLUMN_COUNT = 9;

        private void Start()
        {
            GenerateLanes();
        }

        //TODO: Make method cleaner and more readable
        private void GenerateLanes()
        {
            float offsetY = _gridOriginPosition.y;
        
            for (int row = 0; row < BUILDABLE_AREA_GRID_ROW_COUNT; row++)
            {
            
                float offsetX = _gridOriginPosition.x;

                for (int column = 0; column < BUILDABLE_AREA_GRID_COLUMN_COUNT; column++)
                {
                    Instantiate(_tilePrefab, new Vector3(offsetX, offsetY, CameraInputLayer.PRIORITY_TILE), 
                        Quaternion.identity, _tileParent);

                    offsetX += _tilePrefab.transform.localScale.x;
                }
            
                //TODO: Find a cleaner way to get the wave's Y center point
                GenerateLawnMowers(_tileParent.transform.GetChild(_tileParent.transform.childCount - 1).transform
                    .position.y);

                offsetY += _tilePrefab.transform.localScale.y;
            }
        }

        private void GenerateLawnMowers(float currentRowYCenterPosition)
        {
            float offsetX = GetLeftmostTileXPosition() - _tilePrefab.transform.localScale.x;

            var lawnMowerSpawnPosition = new Vector3(offsetX, currentRowYCenterPosition, 0f);

            LawnMower lawnMower = Instantiate(_lawnMowerPrefab, lawnMowerSpawnPosition, Quaternion.identity, _lawnMowersContainer);
            int spriteSortingOrderLogicOffset = 10;
            lawnMower.SpriteRenderer.sortingOrder = spriteSortingOrderLogicOffset;
        }

        private float GetLeftmostTileXPosition()
        {
            return _tileParent.transform.GetChild(0).transform.position.x;
        }

        private float GetRightmostTileXPosition()
        {
            return _tileParent.transform.GetChild(BUILDABLE_AREA_GRID_COLUMN_COUNT - 1).transform.position.x;
        }
    }
}
