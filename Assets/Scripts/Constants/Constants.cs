using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GAME_STATES
{
    START,
    WAVE_SHOP,
    PLAYING,
    GAME_OVER,
    END_GAME
}

public enum ATTACK_STAGE
{
    START,
    DELAY,
    DURATION,
    FINISHED
}

public enum RARITY
{
    TIER_1,
    TIER_2,
    TIER_3,
    TIER_4,
}

public enum ITEM_TYPE
{
    ITEM,
    WEAPON
}

public enum DROPPED_ITEM_TYPE
{
    GOLD,
    FRUIT,
    CRATE,
    LENGENDARY_CRATE
}

public enum ENEMY_TYPE
{
    MONSTER,
    RAT,
    BAT
}

public enum HIT_EFFECT_TYPE
{
    DAMAGE_EFFECT,
    BLOOD_EFFECT,
    FIRE_EFFECT
}

public enum ITEM_TIER
{
    TIER_1,
    TIER_2,
    TIER_3,
    TIER_4
}

public enum GAME_OVER_TYPE {
    WON,
    LOST,
    ENDLESS
}

public enum CHARACTER_SELECT_STATES {
    SELECT_STATE,
    READY_STATE,
    BUY_STATE
}

public enum PAYMENT_ITEM_TYPE {
    CONSUMABLE,
    NON_CONSUMABLE,
    SUBSCRIPTION
}