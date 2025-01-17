using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterInfoListController : MonoBehaviour
{
    [SerializeField] private List<GameObject> statListPooling;
    [SerializeField] private GameObject statsImage;
    [SerializeField] private GameObject contentObject;
    private Dictionary<string, CharacterStat> validateStatsList;

    private void Awake()
    {
        statListPooling = new List<GameObject>();
    }

    public void Render()
    {
        DisableActiveStatItem();
        foreach(var stats in validateStatsList){
            CreateStatsItem(stats);
        }
    }

    public void LoadData(Character_Mod data)
    {
        validateStatsList = data.GetProperties()
        .Where(x => x.Value.Value > 0)
        .ToDictionary(x => x.Key, x => x.Value);
        Render();
    }

    private void CreateStatsItem(KeyValuePair<string, CharacterStat> statPair){
        GameObject itemInstance = null;
        var imageUseable = statListPooling.Where(x => !x.activeInHierarchy).ToList();
        if(imageUseable.Count == 0){
            itemInstance = Instantiate(statsImage, contentObject.transform);
            var controller = itemInstance.GetComponent<CharacterBaseStatsDisplay>();
            controller.LoadData(statPair.Key, statPair.Value.Value);
            statListPooling.Add(itemInstance);
        }else{
            var controller = imageUseable[0].GetComponent<CharacterBaseStatsDisplay>();
            controller.LoadData(statPair.Key, statPair.Value.Value);
        }
    }

    private void DisableActiveStatItem(){
        var activeList = statListPooling.Where(x => x.activeInHierarchy).ToList();
        foreach(var item in  activeList) item.GetComponent<CharacterBaseStatsDisplay>().DisableRender();
    }
}
