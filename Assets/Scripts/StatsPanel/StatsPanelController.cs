using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StatsPanelController : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;
    [SerializeField] private RectTransform transform;


    private CharacterStat[] stats;


    private void OnEnable()
    {
        DisplayStatsPanel(true);
    }
    private void OnDisable()
    {
        DisplayStatsPanel(false);
    }

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        if (stats.Length > statDisplays.Length)
        {
            Debug.LogError("Not Enough Stat Displays!");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].Stat = i < stats.Length ? stats[i] : null;
            statDisplays[i].gameObject.SetActive(i < stats.Length);
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].ValueText.text = stats[i].Value.ToString();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].NameText.text = statNames[i];
        }
    }

    private void DisplayStatsPanel(bool status){
        if(status) transform.DOScale(Vector3.one, 3f);
        else transform.DOScale(Vector3.zero, 3f);
    }
} 

