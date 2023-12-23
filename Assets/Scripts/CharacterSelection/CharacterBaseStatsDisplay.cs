using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBaseStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro valueText;
    [SerializeField] private float value;

    public void LoadData(float value)
    {
        if((int) value != value)
        {
            this.value = value * 100;
        }
        else
        {
            this.value = value;
        }
        Render();
    }

    public void Render()
    {
        valueText.text = value.ToString();
        this.gameObject.SetActive(true);
    }

    public void DisableRender()
    {
        value = 0;
        valueText.text = "";
        this.gameObject.SetActive(false);
    }
}
