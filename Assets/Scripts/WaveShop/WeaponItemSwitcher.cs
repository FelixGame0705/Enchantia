using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject weaponBar;
    [SerializeField] private GameObject itemBar;
    [SerializeField] private Image _weaponBtnImage;
    [SerializeField] private Image _itemBtnImage;
    [SerializeField] private BAR_STATE StateBar { get; set; }

    [SerializeField] private Color _unactiveColor;
    [SerializeField] private Color _activeColor;
    public enum BAR_STATE { WEAPON, ITEM}

    private void OnEnable()
    {
        ChangeBar(BAR_STATE.WEAPON);
    }

    private void ChangeBtnBackgroundBaseOnEnum()
    {
       switch (StateBar)
       {
            case BAR_STATE.ITEM:
                _itemBtnImage.color = _activeColor;
                _weaponBtnImage.color = _unactiveColor;
                break;

            case BAR_STATE.WEAPON:
                _itemBtnImage.color = _unactiveColor;
                _weaponBtnImage.color = _activeColor;
                break;
       }
    }
    public void ChangeBar(BAR_STATE state)
    {
        StateBar = state;
        switch(state)
        {
            case BAR_STATE.ITEM:
                weaponBar.SetActive(false);
                itemBar.SetActive(true);
                    break;
            case BAR_STATE.WEAPON:
                weaponBar.SetActive(true);
                itemBar.SetActive(false);
                break;
        }
        ChangeBtnBackgroundBaseOnEnum();
    }

    public void OnWeaponBtnClicked()
    {
        ChangeBar(BAR_STATE.WEAPON); 
    }
    public void OnItenBtnClicked()
    {
        ChangeBar(BAR_STATE.ITEM);
    }
}
