using System.Collections.Generic;
using UnityEngine;

public class GameOverStatsDisplayController : MonoBehaviour
{
    [SerializeField] private List<GameOverStatsDisplay> characterBaseStatsDisplays;

    [SerializeField] private Character_Mod characterStats;
    [SerializeField] private List<float> valueList;

    private void Render()
    {
        int index = 0;
        foreach (var character in characterBaseStatsDisplays) {
            character.LoadData(valueList[index]);
            index++;
        }
    }

    public void LoadData(Character_Mod characterData)
    {
        characterStats = characterData;
        ConvertToList();
        Render();
    }

    private void ConvertToList()
    {
        valueList = new List<float>
        {
            characterStats.MaxHP.BaseValue,
            characterStats.HPRegeneration.BaseValue,
            characterStats.LifeSteal.BaseValue,
            characterStats.Damage.BaseValue,
            characterStats.MeleeDamage.BaseValue,
            characterStats.RangedDamage.BaseValue,
            characterStats.ElementalDamage.BaseValue,
            characterStats.AttackSpeed.BaseValue,
            characterStats.CritChance.BaseValue,
            characterStats.Engineering.BaseValue,
            characterStats.Range.BaseValue,
            characterStats.Armor.BaseValue,
            characterStats.Dodge.BaseValue,
            characterStats.Speed.BaseValue,
            characterStats.Luck.BaseValue,
            characterStats.Harvesting.BaseValue,
            characterStats.HPRegeneration.BaseValue
        };
    }
}
