using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DetailWeapon : MonoBehaviour
{
    [SerializeField] private Text detailTxt;
    [SerializeField] private int id_combine;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {

    }

    public void SetDetailTxt(string content)
    {
        detailTxt.text = content;
    }

    public void OnClickRecycle()
    {
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        //ShopController.Instance.GetWeaponCardSystem().RemoveCard(ShopController.Instance.GetIndexWeaponSelected());
        //ShopController.Instance.GetPlayerUpdown().GetWeaponSytem().RemoveWeapon(ShopController.Instance.GetIndexWeaponSelected());
        gameObject.SetActive(false);
    }

    // 
    public void OnClickCombine()
    {
        int oldIndex = WaveShopMainController.Instance.GetIndexWeaponSelected();
        int oldID = WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[oldIndex].GetCardData().Id;
        var indexRemove = FindIndexRemove(oldIndex,oldID);
        if (FindIndexRemove(oldIndex, oldID) < 0) return;
        WaveShopMainController.Instance.GetWeaponInventory().UpgradeCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().UpgradeWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(indexRemove);
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(indexRemove);
    }

    private int FindIndexRemove(int oldIndex, int oldID)
    {
        int id_combine = -1;
        var cardControllerList = WaveShopMainController.Instance.GetWeaponInventory().CardControllerList;
        var cardImage = WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[oldIndex];
        if(cardImage == null) return -1;
        if(cardImage.GetCardData().NextItemWeapon == null) return -1;
        var list = cardControllerList.FindAll(item => {
            return item.GetCardData().name.Equals(cardImage.GetCardData().name) 
            && item.GetCardData().Tier == cardImage.GetCardData().Tier
            && item.GetID() != cardImage.GetID()
            ;
        });
        if(list.Count > 0) id_combine = list[0].GetID();
        // for (int i = 0; i < cardControllerList.Count; i++)
        // {
        //     if (cardControllerList[i].GetCardData().Id == oldID)
        //     {
        //         if (cardControllerList[i].GetID() != oldIndex)
        //         {
        //             cardControllerList[i].GetID();
        //         }
        //     }

        //     if (WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[i].GetCardData().NextItemWeapon == null) continue;
        // }
        return id_combine;
    }
}
