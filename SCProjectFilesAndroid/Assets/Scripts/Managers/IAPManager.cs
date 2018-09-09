using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour , IStoreListener
{
    [SerializeField] Text _polaroidsText;

    public void OnInitialized(IStoreController storeController , IExtensionProvider extensionsProvider){}
	public void OnInitializeFailed(InitializationFailureReason initializationFailedMessage){}
	public void OnPurchaseFailed(Product product , PurchaseFailureReason purchaseFailedMessage) {}
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    { 
        return PurchaseProcessingResult.Complete;
    }

	public void PurchaseButton(Product product)
    {
        _polaroidsText.enabled = false;

        if(product != null)
        {
            switch(product.definition.id)
            {
                case "item01":
                    _polaroidsText.text = product + "Purchased :)";
                    _polaroidsText.enabled = true;
                break;

                case "item02":
                    _polaroidsText.text = "Smartphone 02 Purchased :)";
                    _polaroidsText.enabled = true;
                break;

                case "item03":
                    _polaroidsText.text = "Smartphone 03 Purchased :)";
                    _polaroidsText.enabled = true;
                break;

                default:
                    Debug.LogError("Sir Bhanu, Please check the ID again :)");
                    _polaroidsText.text = "Oops!! Something went wrong :( Please try again :)";
                    _polaroidsText.enabled = true;
                break;
            }
        }
    }

    public void PurchaseFailedButton()
    {
        _polaroidsText.text = "Oops!! Something went wrong :( Please try again :)";
        _polaroidsText.enabled = true;
    }
}
