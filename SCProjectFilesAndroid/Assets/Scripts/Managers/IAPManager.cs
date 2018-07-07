using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour , IStoreListener
{
    [SerializeField] Text _iapText;

    public static int _iapFullContinueDeathsAvailable , _iapHalfContinueDeathsAvailable , _iapThreeQuartersContinueDeathsAvailable;

	void Awake()
    {
        ConfigurationBuilder cb = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        UnityPurchasing.Initialize(this , cb);
    }

	public void IAPContinuePurchaseButton(Product product)
    {
        _iapText.enabled = false;

        if(product != null)
        {
            switch(product.definition.id)
            {
                case "continue0.5":
                    _iapHalfContinueDeathsAvailable += 5;
                    BhanuPrefs.SetIAPHalfContinueDeaths(_iapHalfContinueDeathsAvailable);
                    _iapText.text = "50% Continue Purchased :) \n You can now use this " + _iapHalfContinueDeathsAvailable + " times before purchasing again :)";
                    _iapText.enabled = true;
                break;

                case "continue0.75":
                    _iapThreeQuartersContinueDeathsAvailable += 5;
                    BhanuPrefs.SetIAPThreeQuartersContinueDeaths(_iapThreeQuartersContinueDeathsAvailable);
                    _iapText.text = "75% Continue Purchased :) \n You can now use this " + _iapThreeQuartersContinueDeathsAvailable + " times before purchasing again :)";
                    _iapText.enabled = true;
                break;

                case "continuefull":
                    _iapFullContinueDeathsAvailable += 5;
                    BhanuPrefs.SetIAPFullContinueDeaths(_iapFullContinueDeathsAvailable);
                    _iapText.text = "100% Continue Purchased :) \n You can now use this " + _iapFullContinueDeathsAvailable + " times before purchasing again :)";;
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
