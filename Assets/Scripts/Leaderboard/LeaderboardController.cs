using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform _leaderBoardContainer;
    [SerializeField] private LeaderboardItem _leaderBoardItemPrefab;
    [SerializeField] private PaginationBarDisplayController _paginationBarDisplayController;
    [SerializeField] private int page;
    [SerializeField] private int pageSize;
    [SerializeField] private int maxPage;
    // private List<LeaderboardItemData> dataSample = new List<LeaderboardItemData> {
    //     new LeaderboardItemData(1,"Son1",100,1,9999), 
    //     new LeaderboardItemData(2,"Son2",100,4,9998), 
    //     new LeaderboardItemData(3,"Son3",100,1,9988), 
    //     new LeaderboardItemData(4,"Son4",100,8,9888), 
    //     new LeaderboardItemData(5,"Son5",100,7,8888), 
    //     new LeaderboardItemData(6,"Son6",100,6,8887), 
    //     new LeaderboardItemData(7,"son7",100,5,8880), 
    // };

    [SerializeField] private List<LeaderboardItem> items;

    private async void Awake()
    {
        items = new();
        page = 0;
        pageSize = 5;
        
    }

    public void RenderLeaderBoard(){
        if(!gameObject.activeInHierarchy){
            rectTransform.localScale = new Vector3(0, 0, 0);
            CleanLeaderBoard();
            gameObject.SetActive(true);
            // foreach(var item in dataSample){
            //     FireBaseManager.Instance.LeaderboardService.AddNewLeaderBoard(item);
            // }
            rectTransform.DOScale(1f,0.5f).OnComplete(LoadDataFromFireBase);
        }else {
            CleanLeaderBoard();
            rectTransform.DOScale(0f, 0.5f);
            gameObject.SetActive(false);
        }
    }

    public async void OnOpen(List<LeaderboardItemData> datas)
    {   
        List<Task> taskList = new();
        maxPage = (int) await FireBaseManager.Instance.LeaderboardService.GetMaxPage(pageSize);
        foreach (var data in datas)
        {
            var newItems = Instantiate(_leaderBoardItemPrefab, _leaderBoardContainer);
            newItems.SetData(data);
            var rect = newItems.GetComponent<RectTransform>();
            rect.localScale = new Vector3(0, 0, 0);
            taskList.Add(rect.DOScale(1f, 0.5f).AsyncWaitForCompletion());
            items.Add(newItems);
        }
        await Task.WhenAll(taskList);
        ValidatePageBar();
    }

    public async void CleanLeaderBoard()
    {
        List<Task> taskList = new();
        if(items.Count != 0){
            foreach (var item in items)
            {
                var rect = item.GetComponent<RectTransform>();
                taskList.Add(rect.DOScale(0f, 0.5f).OnComplete(() => Destroy(item.gameObject)).AsyncWaitForCompletion());
            }
            items.Clear();
            await Task.WhenAll(taskList);
        }
    }

    public void GoToNextPage(){
        CleanLeaderBoard();
        page ++;
        LoadDataFromFireBase();
    }

    public void GoToPreviousPage(){
        CleanLeaderBoard();
        page --;
        LoadDataFromFireBase();
    }

    private void LoadDataFromFireBase(){
        FireBaseManager.Instance.LeaderboardService.GetLeaderBoardWithPagination(page,pageSize, OnOpen);
        
    }

    private void ValidatePageBar(){
        _paginationBarDisplayController.UpdateDisplayPagination(page,maxPage);
    }
}
