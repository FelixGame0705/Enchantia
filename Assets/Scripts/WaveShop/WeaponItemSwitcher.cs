using UnityEngine;

public class WeaponItemSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject weaponBar;
    [SerializeField] private GameObject itemBar;
    [SerializeField] private BAR_STATE StateBar { get; set; }
    public enum BAR_STATE { WEAPON, ITEM}

    private void OnEnable()
    {
        ChangeBar(BAR_STATE.WEAPON);
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
