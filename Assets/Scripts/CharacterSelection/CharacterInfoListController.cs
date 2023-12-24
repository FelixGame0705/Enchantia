using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterInfoListController : MonoBehaviour
{
    [SerializeField] private List<CharacterBaseStatsDisplay> statsController;
    private List<float> characterStats = new List<float>();

    public void Render()
    {
        int count = 0;
        foreach(CharacterBaseStatsDisplay display in statsController)
        {
            display.LoadData(characterStats[count]);
            count++;
        }
    }

    public void LoadData(Character_Mod data)
    {
        characterStats.Add(data.MaxHP.Value);
        characterStats.Add(data.Damage.Value);
        characterStats.Add(data.AttackSpeed.Value);
        characterStats.Add(data.Armor.Value);
        characterStats.Add(data.Speed.Value);
        Render();
    }
}
