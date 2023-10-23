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
    GAME_OVER
}

public enum ATTACK_STAGE
{
    START,
    DURATION,
    FINISHED,
    END
}

public enum RARITY
{
    TIER_1,
    TIER_2,
    TIER_3,
    TIER_4,
}
