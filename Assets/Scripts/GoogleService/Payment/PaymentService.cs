using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PaymentService : MonoBehaviour, IDetailedStoreListener
{
    IStoreController storeController;
    IExtensionProvider extensionProvider;
    [SerializeField] List<PaymentData> paymentDataList;
    private void Start()
    {
        GameDataController.Instance.GoogleUserData.Gem = PlayerPrefs.GetInt("totalGems");
        paymentDataList.ForEach(x => {
            SetLocalizedPrice(x);
        });
        SetupPaymentBuilder();
        CheckSubscriptionProduct("no_ads");
    }

    private void SetupPaymentBuilder(){
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        AddProduct(builder);
        UnityPurchasing.Initialize(this, builder);
    }

    void AddProduct(ConfigurationBuilder builder){
        paymentDataList.ForEach(x => {
            switch(x.PaymentType){
                case PAYMENT_ITEM_TYPE.CONSUMABLE:
                builder.AddProduct(x.PaymentInfo.Id, ProductType.Consumable);
                break;
                case PAYMENT_ITEM_TYPE.NON_CONSUMABLE:
                builder.AddProduct(x.PaymentInfo.Id, ProductType.NonConsumable);
                break;
                case PAYMENT_ITEM_TYPE.SUBSCRIPTION:
                builder.AddProduct(x.PaymentInfo.Id, ProductType.Subscription);
                break;
            }
        });
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var purchaseProduct = purchaseEvent.purchasedProduct;
        var productInfo = paymentDataList.Find(o => o.PaymentInfo.Id == purchaseProduct.definition.id);
        if(productInfo == null) return PurchaseProcessingResult.Complete;
        switch(productInfo.PaymentType){
            case PAYMENT_ITEM_TYPE.CONSUMABLE:
            string receipt = purchaseProduct.receipt;
            var data = JsonUtility.FromJson<Data>(receipt);
            var payLoad = JsonUtility.FromJson<Payload>(data.Payload);
            var payLoadData = JsonUtility.FromJson<PayloadData>(payLoad.json);
            int quantity = payLoadData.quantity;
            GameDataController.Instance.GoogleUserData.Gem = quantity * productInfo.PaymentInfoClient.value;
            break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log(failureDescription);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(failureReason);
    }


    public void Purchase(PaymentData payment){
        storeController.InitiatePurchase(payment.PaymentInfo.Id);
    }

    void CheckSubscriptionProduct(string id){
        var subProduct = storeController.products.WithID(id);
        if(subProduct != null){
            try{
                if(subProduct.hasReceipt){
                    var subManager = new SubscriptionManager(subProduct,null);
                    var information = subManager.getSubscriptionInfo();
                    print(information.getExpireDate());
                    if(information.isSubscribed() == Result.True){
                        GameDataController.Instance.GoogleUserData.Ads = true;
                        print("Ok");
                    }else{
                        GameDataController.Instance.GoogleUserData.Ads = false;
                        print("Failed");
                    }
                }
            }catch(Exception){
                Debug.Log("");
            }
        }
    }
    public void SetLocalizedPrice(PaymentData data)// Product id
    {
        Product p = storeController.products.WithID(data.PaymentInfo.Id);
        decimal price = p.metadata.localizedPrice;
        string code = p.metadata.isoCurrencyCode;
        data.PaymentInfo.priceUnit = code;
        data.PaymentInfo.price = price;
    }
}


[Serializable]
public class Payload {
    public string json;
    public string signature;
    public List<SkuDetails> skuDetails;
    public PayloadData payloadData;
}
[Serializable]
public class PayloadData {
    public string orderId;
    public string packageName;
    public string productId;
    public long purchaseTime;
    public int purchaseState;
    public string purchaseToken;
    public int quantity;
    public bool acknowledged;
}
[Serializable]
public class SkuDetails {
    public string productId;
    public string type;
    public string title;
    public string name;
    public string iconUrl;
    public string description;
    public string price;
    public long price_amount_micros;
    public string price_currency_code;
    public string skuDetailsToken;
}

[Serializable]
public class Data {
    public string Payload;
    public string Store;
    public string TransactionID;
}