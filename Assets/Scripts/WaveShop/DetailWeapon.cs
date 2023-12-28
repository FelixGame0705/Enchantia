using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void OnClickCombine()
    {
        int oldIndex = WaveShopMainController.Instance.GetIndexWeaponSelected();
        int oldID = WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[oldIndex].GetCardData().Id;
        if (FindIndexRemove(oldIndex, oldID) < 0) return;
        WaveShopMainController.Instance.GetWeaponInventory().UpgradeCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().UpgradeWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(FindIndexRemove(oldIndex, oldID));
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(FindIndexRemove(oldIndex, oldID));
    }

    private int FindIndexRemove(int oldIndex, int oldID)
    {
        int id_combine = -1;
        var cardControllerList = WaveShopMainController.Instance.GetWeaponInventory().CardControllerList;
        for (int i = 0; i < cardControllerList.Count; i++)
        {
            if (cardControllerList[i].GetCardData().Id == oldID)
            {
                if (cardControllerList[i].GetID() != oldIndex)
                {
                    cardControllerList[i].GetID();
                }
            }

            if (WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[i].GetCardData().NextItemWeapon == null) continue;
        }
        return id_combine;
    }
}
