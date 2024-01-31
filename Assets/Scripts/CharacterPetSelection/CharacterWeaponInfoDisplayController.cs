using UnityEngine;

public class CharacterWeaponInfoDisplayController : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private WeaponSkillDisplayController weaponSkillDisplayController;
    [SerializeField] private CharacterSelectDisplayController characterSelectDisplayController;
    [SerializeField] private CharacterBaseInfo _characterBaseInfo;

    public CharacterSelectDisplayController CharacterSelectDisplayController {get => this.characterSelectDisplayController;}
    public WeaponSkillDisplayController WeaponSkillDisplayController { get => this.weaponSkillDisplayController;}
    
    
    public void LoadData(CharacterBaseInfo characterBaseInfo){
        this._characterBaseInfo = characterBaseInfo;
        RenderUI();
    }

    public void RenderUI(){
        weaponSkillDisplayController.LoadData(_characterBaseInfo);
        characterSelectDisplayController.LoadData(_characterBaseInfo);
    }

    public void DisableUI(){
        weaponSkillDisplayController.HiddenUI();
        characterSelectDisplayController.HiddenUI();
    }
}