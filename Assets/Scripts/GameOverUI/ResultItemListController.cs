using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemListController : MonoBehaviour
{
    [SerializeField] private GameObject _objectModel;
    [SerializeField] private GameObject _weaponLists;
    [SerializeField] private GameObject _itemLists;


    public void Render()
    {
        var weaponList = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        var itemList = WaveShopMainController.Instance.GetWeaponInventory().ItemControllerList;
        RenderWeaponListWithAnimation(weaponList);
        RenderItemListWithAnimation(itemList);
    }


    private async void RenderWeaponListWithAnimation(List<ItemImageController> weaponList){
        var taskList = new List<Task>();
        foreach(var card in weaponList){
            var cloneCard = Instantiate(_objectModel, _weaponLists.transform);
            cloneCard.transform.localScale = new Vector3(0,0,0);
            cloneCard.GetComponent<Image>().sprite = card.GetCardData().ItemImg;
            taskList.Add(cloneCard.transform.DOScale(1f, 2f).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        }
        await Task.WhenAll(taskList);
    }

    private async void RenderItemListWithAnimation(List<ItemImageController> itemList){
        var taskList = new List<Task>();
        foreach (var card in itemList)
        {
            var cloneCard = Instantiate(_objectModel, _itemLists.transform);
            cloneCard.transform.localScale = new Vector3(0,0,0);
            cloneCard.GetComponent<Image>().sprite = card.GetCardData().ItemImg;
            taskList.Add(cloneCard.transform.DOScale(1f, 2f).SetEase(Ease.OutBounce).AsyncWaitForCompletion());
        }
        await Task.WhenAll(taskList);
    }   
}
