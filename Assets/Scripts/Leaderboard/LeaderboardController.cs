using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public List<LeaderboardItemData> datas = new List<LeaderboardItemData> {
        new LeaderboardItemData(1,"Son1",100,11,9999), 
        new LeaderboardItemData(2,"Son2",100,10,9998), 
        new LeaderboardItemData(3,"Son3",100,9,9988), 
        new LeaderboardItemData(4,"Son4",100,8,9888), 
        new LeaderboardItemData(5,"Son5",100,7,8888), 
        new LeaderboardItemData(6,"Son6",100,6,8887), 
        new LeaderboardItemData(7,"son7",100,5,8880), 
    };

    public List<LeaderboardItem> items = new List<LeaderboardItem>();
    public Transform Content;
    public LeaderboardItem Prefab;

    public void OnOpen()
    {
        if(items.Count > 0)
        {
            foreach (var item in items)
            {
                Destroy(item.gameObject);
            }
            items.Clear();
        }    

        foreach (var data in datas)
        {
            var newItems = Instantiate(Prefab, Content);
            newItems.SetData(data);
            items.Add(newItems);
        }
    }
}
