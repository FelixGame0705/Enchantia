using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaginationBarDisplayController : MonoBehaviour
{
    [SerializeField] private Button backAllBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button forwardBtn;
    [SerializeField] private Button forwardAllBtn;
    [SerializeField] private TMP_Text pageNum;

    private void Awake(){
        pageNum.text = "0";
    }

    public void UpdateDisplayPagination(int page, int maxPage){
        pageNum.text = page.ToString();
        if(page == 0){
            if(page == maxPage){
                ChangeActiveBackBtn(false);
                ChangeActiveForwardBtn(false);
            }else{
                ChangeActiveBackBtn(false);
            }
        }else if(page == maxPage){
            ChangeActiveBackBtn(false);
        }else {
            ChangeActiveBackBtn(true);
            ChangeActiveForwardBtn(true);
        }
    }

    private void ChangeActiveBackBtn(bool state){
        backAllBtn.interactable = state;
        backBtn.interactable = state;
    }

    private void ChangeActiveForwardBtn(bool state){
        forwardBtn.interactable = state;
        forwardAllBtn.interactable = state;
    }
}