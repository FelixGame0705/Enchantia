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
