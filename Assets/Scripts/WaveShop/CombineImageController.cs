using UnityEngine;
using UnityEngine.UI;

public class CombineImageController : MonoBehaviour
{
    [SerializeField] private Sprite spriteImage;
    [SerializeField] private int index;
    [SerializeField] private Image imageRenderer;
    [SerializeField] private Image background;
    [SerializeField] private int tier;

    public int Index {get => this.index;}


    public void Render(){
        imageRenderer.sprite = spriteImage;
        var configTier = WaveShopMainController.Instance.CombindPanelController.GetTierCardSpriteInfoByTier(tier);
        this.background.sprite = configTier.IconBackground;
        this.gameObject.SetActive(true);
    }

    public void LoadData(int index, Sprite sprite, int tier){
        this.index = index;
        this.spriteImage = sprite;
        this.tier = tier;
        Render();
    }

    public void DisableItem(){
        this.gameObject.SetActive(false);
    }

    public void OnImageClicked(){
        var controller = WaveShopMainController.Instance.CombindPanelController;
        controller.HandleItemClicked(index);
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        spriteImage = null;
        imageRenderer.sprite = null;
        index = -1;
    }
}