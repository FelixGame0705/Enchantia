using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PaymentData")]
public class PaymentData : ScriptableObject
{
    [SerializeField] private PAYMENT_ITEM_TYPE paymentType;
    [SerializeField] private PaymentInfo paymentInfo;
    [SerializeField] private PaymentInfoClient paymentInfoClient;

    public PAYMENT_ITEM_TYPE PaymentType { get { return paymentType; } }
    public PaymentInfo PaymentInfo { get { return paymentInfo;}}
    public PaymentInfoClient PaymentInfoClient {get { return paymentInfoClient;}}
}

[Serializable]
public class PaymentInfo {
    public string Name;
    public string Id;
    public string desc;
    public decimal price;
    public float timeDuration;
    public string priceUnit;
}
[Serializable]
public class PaymentInfoClient {// if there is gem, value is > 0 for consumable item
    public string description;
    public string name;
    public int value;
}