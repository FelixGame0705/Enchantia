using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollPlayController : MonoBehaviour
{
    [SerializeField] private Button _rerollBtn;
    [SerializeField] private Text _rerollPriceText;

    [SerializeField] private bool _rerollValid = true;
    
    public void Play()
    {
        GamePlayController.Instance.UpdateState(GAME_STATES.PLAYING);
    }
    public void Reroll()
    {
        WaveShopMainController.Instance.Reroll();
    }
    public void ChangeRerollBtnState(bool state)
    {
        if(state != _rerollValid){
            _rerollValid = state;
            _rerollBtn.interactable = state;
        }
    }

    public void ChangeRerollPriceUI(int price){
        _rerollPriceText.text = price.ToString();
    }

}
