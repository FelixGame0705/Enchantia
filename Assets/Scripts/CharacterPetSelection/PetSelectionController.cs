using System.Collections.Generic;
using UnityEngine;

public class PetSelectionController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _petLists;
    [SerializeField] private GameObject _basePetImage;
    [SerializeField] private GameObject _contentList;
    [SerializeField] private IItemImageController _selectedImage;
    [SerializeField] private List<IItemImageController> _petImageList;
    [SerializeField] private ContentScale _contentScale;
    [SerializeField] private Sprite _backgroundDefault;
    [SerializeField] private Sprite _backgroundSelected;

    public Sprite BackgroundDefault { get => _backgroundDefault; }
    public Sprite BackgroundSelected { get => _backgroundSelected; }
    public List<GameObject> PetList { get => _petLists; set => _petLists = value; }
    public IItemImageController SelectedImage {get => this._selectedImage;}
    public void HandlePetClicked(int index)
    {
        _selectedImage = _petImageList[index];
        // CharacterSelectionController.Instance.SetSelectedCharacter(_characterList[index]);
        CharacterSelectionControllerManagement.Instance.CharacterPetSelectionController.SetSelectedPet(_petLists[index]);
        CharacterSelectionControllerManagement.Instance.CharacterPetSelectionController.CheckIsReady();
    }
    public void ResetBackgroundSelectedCharacterToDefault()
    {
        if(_selectedImage != null)
        {
            _selectedImage.ChangeBackgroundToDefault();
        }
    }
    public void PetListInit(){
        _petImageList = new List<IItemImageController>();
        int count = 0;
        foreach (var item in _petLists)
        {
            var clone = Instantiate(_basePetImage, _contentList.transform);
            var petImageControl = clone.GetComponent<PetImageController>();
            _petImageList.Add(petImageControl);
            var petBaseInfo = item.GetComponent<PetBaseInfo>();
            petImageControl.LoadData(count, petBaseInfo.CharacterSprite, true);
            count++;
        }
        _contentScale.AdjustSize(_petImageList.Count);
    }
}