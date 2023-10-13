using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    List<WeaponBase> weaponBases = new List<WeaponBase>();

    public void EquipedWeapon(WeaponBase weapon)
    {
        weaponBases.Add(weapon);
    }

    public void SellWeapon(WeaponBase weapon)
    {
        weaponBases.Remove(weapon);
    }

    public void CombineWeapon(WeaponBase weaponCombine, WeaponBase newWeapon)
    {
        for(int i = 0; i < 2; i++)
        {
            if(weaponCombine.weaponData.WeaponId.Equals(weaponBases[i].weaponData.WeaponId))
            SellWeapon(weaponCombine);
        }
        
        EquipedWeapon(newWeapon);
    }

    public void Attack()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
