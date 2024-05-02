using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectDisplayController : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private GameObject _characterSelectedDisplay;
    [SerializeField] private CharacterInfoListController characterInfoListController;
    [SerializeField] Character_Mod _selectedCharacterData;
    [SerializeField] private GameObject _characterObject;
    [SerializeField] private Button _functionBtn;
    [SerializeField] private TMP_Text _functionBtnText;
    [SerializeField] private GameObject _characterClone;
    [SerializeField] private Vector3 _scale;

    [Header ("Configs")]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;


    private bool isFirstRender = false;

    public void Render()
    {
        if(!isFirstRender)
        {
            this.gameObject.SetActive(true);
            isFirstRender = true;
        }else Destroy(_characterClone);
            _characterClone = Instantiate(_characterObject, _characterSelectedDisplay.transform);
            var transform = _characterClone.AddComponent(typeof(RectTransform)) as RectTransform;
            transform.localScale = _scale;
            transform.anchorMin = new Vector2(0.5f, 0);
            transform.anchorMax = new Vector2(0.5f, 0);
            transform.pivot = new Vector2(0.5f,0);
            transform.anchoredPosition3D = new Vector3(0,0,0);
        _characterClone.transform.localScale = _scale;
        characterInfoListController.LoadData(_selectedCharacterData);
    }

    public void LoadData(CharacterBaseInfo data)
    {
        _selectedCharacterData = data.CharacterStats;
        _characterObject = data.CharacterFullAnimation;
        Render();
    }

    public void HiddenUI(){
        
    }

    public void ConfigFunctionBtnStyle(bool isActive, string context){
        if(isActive){
            _functionBtn.targetGraphic.color = activeColor;
            _functionBtn.interactable = true;
        }else{
            _functionBtn.targetGraphic.color = inactiveColor;
            _functionBtn.interactable = false;
        }
        _functionBtnText.text = context;
    }
}
