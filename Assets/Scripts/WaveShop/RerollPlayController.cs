using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollPlayController : MonoBehaviour
{
    [SerializeField] private Button _rerollBtn;
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
        _rerollBtn.interactable = state;
    }

}
