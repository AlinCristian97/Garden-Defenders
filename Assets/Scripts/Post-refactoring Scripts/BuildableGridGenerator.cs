using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildableGridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _tileParent;
    [SerializeField] [Range(80f, 180f)] private float _marginsSideInPixels = 100f;

    private const byte BUILDABLE_AREA_GRID_ROW_COUNT = 5;
    private const byte BUILDABLE_AREA_GRID_COLUMN_COUNT = 9;
    private const float GRID_ASPECT_RATIO = 0.5813953f;
    
    private float _marginsTopBottomInPixels;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;

        _marginsTopBottomInPixels = (GRID_ASPECT_RATIO) * _marginsSideInPixels;
    }

    private void Start()
    {
        GenerateGrid();
        CenterGridByMovingParent();
    }

    /// <summary>
    /// Cheaty way to center the Grid after generating it, by adjusting the parent's Y position
    /// </summary>
    private void CenterGridByMovingParent()
    {
        _tileParent.transform.position = new Vector3(
            0f, 
            Mathf.Abs(_tileParent.GetChild(_tileParent.childCount - 1).transform.position.y + _tileParent.GetChild(0).transform.position.y) / 2f, 
            0f);
    }

    private void GenerateGrid()
    {
        SetTileSize(_tilePrefab);
        
        Vector3 tileScale = _tilePrefab.transform.localScale;
        
        Vector2 offsetY = Vector2.zero;

        for (int row = 0; row < BUILDABLE_AREA_GRID_ROW_COUNT; row++)
        {
            Vector2 offsetX = Vector2.zero;

            for (int column = 0; column < BUILDABLE_AREA_GRID_COLUMN_COUNT; column++)
            {
            
                Instantiate(_tilePrefab, CalculateNewTilePosition(offsetX, offsetY), 
                    quaternion.identity, _tileParent);
                
                offsetX = new Vector2(offsetX.x + tileScale.x, 0f);
            }
            
            offsetY = new Vector2(0f, offsetY.y + tileScale.y);
        }
    }

    private Vector3 CalculateNewTilePosition(Vector3 offsetX, Vector3 offsetY)
    {
        Vector3 newPosition = _cam.ScreenToWorldPoint(
            new Vector3(
                GetScreenPointFromCanvasPoint(new Vector2(_marginsSideInPixels, 0f)).x,
                GetScreenPointFromCanvasPoint(new Vector2(0f, _marginsTopBottomInPixels)).y,
                -_cam.transform.position.z))
                              + new Vector3(GetTileSideHalved().x, GetTileSideHalved().y, 0f)
                              + offsetX
                              + offsetY;

        return newPosition;
    }

    private Vector2 GetTileSideHalved()
    {
        Vector3 tileScale = _tilePrefab.transform.localScale;
        
        return new Vector2(tileScale.x / 2f, tileScale.y / 2f);
    }

    private float GetTileSideSizeInPixelsBySideMargins()
    {
        float result = Mathf.Abs(_cam.pixelWidth - (_marginsSideInPixels * 2)) / 9f;

        return result;
    }
    
    private Vector2 GetScreenPointFromCanvasPoint(Vector2 canvasPoint)
    {
        float widthRatio = GetRatio(Mathf.Abs(canvasPoint.x), _cam.pixelWidth);
        float heightRatio = GetRatio(Mathf.Abs(canvasPoint.y), _cam.pixelHeight);
        
        var result = new Vector2(Screen.width * widthRatio, Screen.height * heightRatio);
        
        return result;
    }

    private float GetRatio(float value, float fromValue) => value / fromValue;

    private void SetTileSize(GameObject tile)
    {
        float tileSideSize = GetTileSideSizeInWorldPoints(GetTileSideSizeInPixelsBySideMargins());
        
        tile.transform.localScale = new Vector3(tileSideSize, tileSideSize, tile.transform.localScale.z);
    }

    private float GetTileSideSizeInWorldPoints(float sideSizeInPixels)
    {
        float sideSizeInWorldPoints =
            _cam.ScreenToWorldPoint(GetScreenPointFromCanvasPoint(new Vector2(sideSizeInPixels, 0f))).x
            - _cam.ScreenToWorldPoint(Vector3.zero).x;

        return sideSizeInWorldPoints;
    }
}