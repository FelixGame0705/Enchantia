using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ContinuePopupController : MonoBehaviour
{
    [SerializeField] private Button _moneyBtn;
    [SerializeField] private Button _watchAdsBtn;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private float _countDown;

    public void RenderUI(){
        if(!gameObject.activeInHierarchy){
            gameObject.SetActive(true);
            rectTransform.DOScale(1f, 0.5f).SetEase(Ease.Linear);
            StartCoroutine(CountDown());
        }else{
            rectTransform.DOScale(0f, 0.5f).SetEase(Ease.Linear);
            gameObject.SetActive(false);
        }
    }

    public void MoneyBtnClicked(){

    }

    public void WatchAdsBtnClicked(){

    }

    IEnumerator CountDown(){
        while(_countDown > 0){
            yield return new WaitForSeconds(1);
            _countDown --;
            UpdateCountDownText();
        }
    }

    private void UpdateCountDownText(){
        countDownText.text = _countDown.ToString() + "S";
    }
}