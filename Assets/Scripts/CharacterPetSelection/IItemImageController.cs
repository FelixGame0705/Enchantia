using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public abstract class IItemImageController : MonoBehaviour
{
    [SerializeField] private Sprite spriteImage;
    [SerializeField] private int index;
    [SerializeField] private Image imageRenderer;
    [SerializeField] private Image background;
    [SerializeField] private bool isValid;

    public Sprite SpriteImage {get => spriteImage; set => spriteImage = value;}
    public int Index {get => this.index; set => index = value;}
    public Image ImageRenderer {get => this.imageRenderer; set => this.imageRenderer = value;}
    public Image Background {get => this.background; set => this.background = value;}
    public bool IsValid {get => this.isValid; set => this.isValid = value;}

    public void Render(){
        imageRenderer.sprite = spriteImage;
        this.gameObject.SetActive(true);
        try{
            var imageBtn = this.gameObject.GetComponent<Button>();
            imageBtn.interactable = isValid;
        }catch(Exception){
            Debug.LogError("Btn is null");
        }
    }

    public abstract void OnImageClicked();
    public abstract void LoadData(int index, Sprite image, bool isValid);

    public abstract void ChangeBackgroundToDefault();

    public void ChangeStatusColor(bool status){
        if(status){
             Background.color = CharacterSelectionControllerManagement.Instance.selectedColor;
        }else  Background.color = CharacterSelectionControllerManagement.Instance.clickedColor;
    }
}