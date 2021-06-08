using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DefenderSpawner : MonoBehaviour
{
    private Defender _defender;

    private void OnMouseDown()
    {
        Debug.Log("Mouse clicked");
        SpawnDefender(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        _defender = defenderToSelect;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);

        Vector2 gridPos = SnapToGrid(worldPos);

        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 roundedPos)
    {
        Defender newDefender = Instantiate(_defender, roundedPos, Quaternion.identity);
        Debug.Log(roundedPos);
    }
}
