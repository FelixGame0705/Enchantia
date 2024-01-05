using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBaseStatsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private float value;
    [SerializeField] private string title;

    public void LoadData(string title,float value)
    {
        this.title = title;
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
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        valueText.text = value.ToString();
        titleText.text = title.ToString();
    }

    public void DisableRender()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        value = 0;
        valueText.text = "";
        titleText.text = "";
        title = "";
    }
}
