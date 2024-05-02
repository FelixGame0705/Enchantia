using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombineRecycleMechanicController : MonoBehaviour
{
    public void RecycleWeapon()
    {
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        WaveShopMainController.Instance.UpdateViewListInfo();
    }

    public void CombineWeapon()
    {
        int oldIndex = WaveShopMainController.Instance.GetIndexWeaponSelected();
        var indexRemove = FindIndexRemove(oldIndex);
        if (FindIndexRemove(oldIndex) < 0) return;
        WaveShopMainController.Instance.GetWeaponInventory().UpgradeCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().UpgradeWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(indexRemove);
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(indexRemove);
        WaveShopMainController.Instance.UpdateViewListInfo();
    }

    public int FindIndexRemove(int oldIndex)
    {
        int id_combine = -1;
        var cardControllerList = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        var cardImage = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList[oldIndex];
        if (cardImage == null) return -1;
        if (cardImage.GetCardData().NextItemWeapon == null) return -1;
        var list = cardControllerList.FindAll(item =>
        {
            return item.GetCardData().name.Equals(cardImage.GetCardData().name)
            && item.GetCardData().Tier == cardImage.GetCardData().Tier
            && item.GetID() != cardImage.GetID()
            ;
        });
        if (list.Count > 0) id_combine = list[0].GetID();
        return id_combine;
    }

    public void AutoCombine()
    {
        var weaponImageList = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        List<List<ItemImageController>> sortList = new List<List<ItemImageController>>();
        weaponImageList.ForEach(item =>
        {
            bool flag = false;
            foreach (var sortItem in sortList)
            {
                    var weaponCloneController = sortItem[0];
                if (weaponCloneController.GetCardData().Tier == item.GetCardData().Tier
                    && weaponCloneController.GetCardData().name.Equals(item.GetCardData().name)
                )
                {
                    sortItem.Add(item);
                    flag = true;
                    break;
                }
            }
            if(!flag){
                var temp = new List<ItemImageController>
                {
                    item
                };
                sortList.Add(temp);
            }
        });
        if (sortList.Any())
        {
            var combineList = sortList.FindAll(item => item.Count > 1);
            if (combineList.Any())
            {
                combineList.ForEach(item =>
                {
                    int index = item[0].GetID();
                    int indexRemove = item[1].GetID();
                    WaveShopMainController.Instance.GetWeaponInventory().UpgradeCard(index);
                    GamePlayController.Instance.GetWeaponSystem().UpgradeWeapon(index);
                    GamePlayController.Instance.GetWeaponSystem().SellWeapon(indexRemove);
                    WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(indexRemove);
                });
            }
        }
    }
    public bool CheckCanAutoCombine(ItemData buyCardData)
    {
        var weaponImageList = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        var result = weaponImageList.FindAll(item =>
        {
            var cloneItemData = item.GetCardData();
            return cloneItemData.name.Equals(buyCardData.name)
            && cloneItemData.Tier == buyCardData.Tier;
        });
        if (result.Count > 0) return true;
        return false;
    }
}