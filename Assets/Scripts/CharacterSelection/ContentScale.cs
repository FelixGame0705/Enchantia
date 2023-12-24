using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentScale : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private RectTransform contentTransform;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float cellBetween = 1f;

    private void Awake()
    {
        cellSize = grid.cellSize.y;
        cellBetween = grid.spacing.y;
        AdjustSize(7);
    }

    public void AdjustSize(int columnEntity)
    {
        var contentHight = columnEntity * (cellSize + cellBetween);
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, contentHight);
    }
}
