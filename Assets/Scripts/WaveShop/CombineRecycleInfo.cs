using UnityEngine;

public class CombineRecycleInfo : MonoBehaviour
{
    // Default combine wave is -1, if the item is combine, it will be set the wave that has been combine
    [SerializeField] private int combineWave;
    [SerializeField] private int buyWave;

    private void Awake()
    {
        combineWave = -1;
        buyWave = 0;
    }

    public int CombineWave{ get  => combineWave; set => combineWave = value;}
    public int  BuyWave {get => buyWave; set => buyWave = value;}
}
