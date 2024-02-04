using UnityEngine;

public class CombineRecycleMechanicController : MonoBehaviour
{
    public void RecycleWeapon()
    {
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
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
    }

    public int FindIndexRemove(int oldIndex)
    {
        int id_combine = -1;
        var cardControllerList = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        var cardImage = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList[oldIndex];
        if(cardImage == null) return -1;
        if(cardImage.GetCardData().NextItemWeapon == null) return -1;
        var list = cardControllerList.FindAll(item => {
            return item.GetCardData().name.Equals(cardImage.GetCardData().name) 
            && item.GetCardData().Tier == cardImage.GetCardData().Tier
            && item.GetID() != cardImage.GetID()
            ;
        });
        if(list.Count > 0) id_combine = list[0].GetID();
        return id_combine;
    }

    public void AutoCombine(){
        int oldIndex = WaveShopMainController.Instance.GetIndexWeaponSelected();
        int oldID = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList[oldIndex].GetCardData().Id;
        var indexRemove = FindIndexRemove(oldIndex);
        if (FindIndexRemove(oldIndex) < 0) return;
        WaveShopMainController.Instance.GetWeaponInventory().UpgradeCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().UpgradeWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(indexRemove);
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(indexRemove);
    }
    public void CheckCanAutoCombine(ItemData buyCardData){

    }
}