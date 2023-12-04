using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : Singleton<Utils>
{
    public float GetAnglesFromVector(Vector3 vectorToCheck)
    {
        float angleInRadians = Mathf.Atan2(vectorToCheck.y, vectorToCheck.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        return angleInDegrees;
    }

    public int CanUpgradeWeapon()
    {
        int count = 1;
        int id_combine = -1;
        for (int i = 0; i < WaveShopMainController.Instance.GetWeaponInventory().CardControllerList.Count; i++)
        {
            for (int j = i; j < WaveShopMainController.Instance.GetWeaponInventory().CardControllerList.Count; j++)
                if (WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[i].GetCardData().Id == WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[j].GetCardData().Id)
                {
                    if(j!=i)
                    if (WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[i].GetID() != WaveShopMainController.Instance.GetIndexWeaponSelected())
                    {
                        id_combine = WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[i].GetID();
                    }

                }

            if (WaveShopMainController.Instance.GetWeaponInventory().CardControllerList[i].GetCardData().NextItemWeapon == null) continue;
        }
        return id_combine;
    }
}
