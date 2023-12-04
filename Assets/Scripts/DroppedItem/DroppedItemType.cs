using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemType : MonoBehaviour
{
    public DroppedItemData DroppedItemData;
    public SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        //spriteRenderer.sprite = DroppedItemData.sprite;
    }

    public void Init() {
        spriteRenderer.sprite = DroppedItemData.sprite;
    }
}
