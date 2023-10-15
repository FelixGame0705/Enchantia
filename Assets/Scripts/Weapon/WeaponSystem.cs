using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]HashSet<WeaponBase> weaponBases = new HashSet<WeaponBase>();
    [SerializeField] List<Transform> weaponTransforms;

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

    public void ExcuteAttack(Transform target, Transform player, bool isCanAttack)
    {
        for (int i = 0; i < weaponBases.Count; i++)
        {
            if (isCanAttack)
            {
                weaponBases.ToArray()[i].SetTargetForAttack(target);
                //weaponBases.ToArray()[i].SetPlayerPosition(player);
            }
        }
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
