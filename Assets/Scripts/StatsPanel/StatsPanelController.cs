using DG.Tweening;
using UnityEngine;

public class StatsPanelController : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;
    [SerializeField] private RectTransform transform;


    private CharacterStat[] stats;

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

    public async void DisplayStatsPanel(bool status){
        if(status && !this.gameObject.activeInHierarchy) {
            transform.anchoredPosition3D = new Vector3(-550,0,0);
            this.gameObject.SetActive(status);
            await transform.DOAnchorPos3D(Vector3.zero, 1f).AsyncWaitForCompletion();
        }else if(!status && this.gameObject.activeInHierarchy){
            await transform.DOAnchorPos3D(new Vector3(-550,0,0), 1f).OnComplete(() => this.gameObject.SetActive(status)).AsyncWaitForCompletion();
        }
    }
} 

