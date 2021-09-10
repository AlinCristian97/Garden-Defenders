using UnityEngine;

namespace General
{
    public class LanesGenerator : MonoBehaviour
    {
        //TODO: Restrict multiple instances via Singleton
    
        [Header("Buildable Grid Tiles")]
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _tileParent;
    
        //TODO: Turn to const after adjustments
        [SerializeField] private Vector2 _gridOriginPosition;

        [Header("Lawn Mower Spawn Points")]
        [SerializeField] private GameObject _lawnMowerSpawnPointPrefab;
        [SerializeField] private Transform _lawnMowerSpawnPointParent;
    
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
                GenerateLawnMowerSpawnPoint(_tileParent.transform.GetChild(_tileParent.transform.childCount - 1).transform
                    .position.y);

                offsetY += _tilePrefab.transform.localScale.y;
            }
        }

        private void GenerateLawnMowerSpawnPoint(float currentRowYCenterPosition)
        {
            float offsetX = GetLeftmostTileXPosition() - _tilePrefab.transform.localScale.x;
        
            Instantiate(_lawnMowerSpawnPointPrefab, new Vector3(offsetX, currentRowYCenterPosition, 0f), 
                Quaternion.identity, _lawnMowerSpawnPointParent);
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
