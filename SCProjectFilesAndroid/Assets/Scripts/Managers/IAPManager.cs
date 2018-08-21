using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour , IStoreListener
{
    [SerializeField] Text _iapText;

	public void IAPContinuePurchaseButton(Product product)
    {
        _iapText.enabled = false;

        if(product != null)
        {
            switch(product.definition.id)
            {
                case "smrtp01":
                    _iapText.text = "Smartphone 01 Purchased :)";
                    _iapText.enabled = true;
                break;

                case "smrtp02":
                    _iapText.text = "Smartphone 02 Purchased :)";
                    _iapText.enabled = true;
                break;

                case "smrtp03":
                    _iapText.text = "Smartphone 03 Purchased :)";
                    _iapText.enabled = true;
                break;

                default:
                    Debug.LogError("Sir Bhanu, Please check the ID again :)");
                    _iapText.text = "Oops!! Something went wrong :( Please try again :)";
                    _iapText.enabled = true;
                break;
            }
        }
    }

    public void IAPFailed()
    {
        _iapText.text = "Oops!! Something went wrong :( Please try again :)";
        _iapText.enabled = true;
    }

	public void OnInitialized(IStoreController storeController , IExtensionProvider extensionsProvider){}
	public void OnInitializeFailed(InitializationFailureReason initializationFailedMessage){}
	public void OnPurchaseFailed(Product product , PurchaseFailureReason purchaseFailedMessage) {}
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e){ return PurchaseProcessingResult.Complete; }
}
