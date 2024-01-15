using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof(GridLayoutGroup), typeof(RectTransform))]
public class ContentScale : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private RectTransform contentTransform;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float cellBetween = 1f;


    private void Start()
    {
        cellSize = grid.cellSize.y;
        cellBetween = grid.spacing.y;
        var allEntity = this.transform.childCount;
        AdjustSize(allEntity);
    }

    public void AdjustSize(int columnEntity)
    {
        var contentHight = GetColumnNumber(columnEntity,contentTransform.sizeDelta.x) * (cellSize + cellBetween);
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, contentHight);
    }

    public int GetColumnNumber(int numberOfEntity, float contentHorizontalSize){
        return Mathf.RoundToInt(numberOfEntity * (cellSize + 2 * cellBetween) / contentHorizontalSize);
    }
}