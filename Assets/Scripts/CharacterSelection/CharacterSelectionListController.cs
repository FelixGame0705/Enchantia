using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class will controll the character list
public class CharacterSelectionListController : MonoBehaviour
{
    [SerializeField] private GameObject _baseCharacterImage;
    [SerializeField] private GameObject _contentList;
    [SerializeField] private IItemImageController _selectedImage;
    [SerializeField] private List<GameObject> _characterList;
    [SerializeField] private List<IItemImageController> _characterImageList;
    [SerializeField] private ContentScale _contentScale;
    [SerializeField] private Sprite _backgroundDefault;
    [SerializeField] private Sprite _backgroundSelected;

    public Sprite BackgroundDefault { get => _backgroundDefault; }
    public Sprite BackgroundSelected { get => _backgroundSelected; }
    public List<GameObject> CharacterList { get => _characterList; set => _characterList = value; }

    //This will take the data from the index given, index is store in each character image
    //and set character select on main controller of this panel
    public void HandleCharacterClicked(int index)
    {
        _selectedImage = _characterImageList[index];
        // CharacterSelectionController.Instance.SetSelectedCharacter(_characterList[index]);
        CharacterSelectionControllerManagement.Instance.CharacterPetSelectionController.SetSelectedCharacter(_characterList[index]);
    }

    public void CharacterListInit()
    {
        _characterImageList = new List<IItemImageController>();
        int count = 0;
        foreach (var item in _characterList)
        {
            var clone = Instantiate(_baseCharacterImage, _contentList.transform);
            var characterImageControl = clone.GetComponent<CharImageController>();
            _characterImageList.Add(characterImageControl);
            var characterBaseInfo = item.GetComponent<CharacterBaseInfo>();
            characterBaseInfo.Load();
            characterImageControl.LoadData(count, characterBaseInfo.CharacterSprite, true);
            count++;
        }
    }
    public void ResetBackgroundSelectedCharacterToDefault()
    {
        if(_selectedImage != null)
        {
            _selectedImage.ChangeBackgroundToDefault();
        }
    }
}
