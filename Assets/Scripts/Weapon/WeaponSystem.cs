using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] List<WeaponBase> weaponBases = new List<WeaponBase>();
    [SerializeField] List<Transform> weaponTransforms;

    public void EquipedWeapon(GameObject weapon)
    {
        foreach(var t in weaponTransforms)
        {
            if(t.childCount == 0)
            {
                GameObject weaponGameObject = Instantiate(weapon, t);
                weaponGameObject.GetComponent<WeaponBase>().SetID(weaponBases.Count);
                weaponBases.Add(weaponGameObject.GetComponent<WeaponBase>());
                break;
            }
        }
    }

    public void SellWeapon(int index)
    {
        Debug.Log("Weapon index " + index);
        if (index < 0) return;
        Destroy(weaponBases[index].gameObject);
        weaponBases.RemoveAt(index);
        InitSetIDWeapon();
        //weaponBases.Remove(weapon);
    }

    private void InitSetIDWeapon()
    {
        for(int i = 0; i < weaponBases.Count; i++)
        {
            weaponBases[i].SetID(i);
        }
    }

    public void UpgradeWeapon(int index)
    {
            weaponBases[index].SetWeaponDataConfig(weaponBases[index].WeaponDataConfig.NextWeapon);
    }

    public WeaponBase GetWeapon(int index)
    {
        return weaponBases[index];
    }

    public int GetCountWeapon()
    {
        return weaponBases.Count;
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
                Debug.Log("Attack");
                weaponBases.ToArray()[i].SetTargetForAttack(target);
                weaponBases.ToArray()[i].SetPlayerPosition(player);
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
