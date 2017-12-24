import UnityEngine.UI;

public class GUIManagerJS extends MonoBehaviour
{
    public var levelManager					: LevelManagerJS;				//A link to the level manager
    public var missionManager				: MissionManagerJS;				//A link to the mission manager
    public var playerManager				: PlayerManagerJS;				//A link to the player manager
    public var powerupManager				: PowerupManagerJS;				//A link to the powerup manager

    public var hangarDistanceText           : TextMesh;                     //A link to the hangar distance text

    public var distanceText					: Text;							//The main UI's distance text
    public var coinText						: Text;							//The main UI's coin ammount text

    public var finishDistanceText			: Text;							//The finish menu's distance text
    public var finishCoinText				: Text;							//The finish menu's coin ammount text

    public var mainUI						: GameObject;					//The main UI

    public var arrowSprites					: Sprite[];						//Up and down arrow sprites
    public var audioSprites					: Sprite[];						//Audio enabled and disabled sprites
    public var ShopSkinButtonSprites		: Sprite[];						//The buy, equip, equipped sprites

    public var overlayAnimator 				: Animator;						//The overlay animator
    public var topMenuAnimator 				: Animator;						//The top menu animator
    public var shopAnimator 				: Animator;						//The shop menu animator
    public var missionMenuAnimator 			: Animator;						//The mission menu animator
    public var pauseMenuAnimator 			: Animator;						//The pause menu animator
    public var finishMenuAnimator 			: Animator;						//The finish menu animator
    public var powerupButtons 				: Animator[];					//The powerup buttons animator (extra speed, shield, sonic wave, revive)
    public var missionNotifications 		: Animator[];					//The mission complete notification panels

    public var missionPanelElements			: RectTransform[];				//The mission panel elements (mission descriptions and mission status)

    public var coinAmmount					: Text;							//The shop menu coin ammount text
    public var ShopOwnedItems				: Text[];						//The shop menu number of owned item texts

    public var shopSubmarineButtons			: Image[];						//The shop menu submarine 1 and 2 buttons
    public var audioButtons					: Image[];						//The audio buttons

    //Tells, which mission notification is used at the moment
    private var usedMissionNotifications	= [false, false, false];

    private var inPlayMode					: boolean = false;
    private var canPause 					: boolean = true;

    private var collectedCoins 				: int = 0;
    private var distanceTraveled 			: int = 0;

    //Called at the beginning of the game
    function Start()
    {
        //Updates the audio buttons sprites
        UpdateAudioButtons();

        hangarDistanceText.text = SaveManagerJS.bestDistance + " M";
        finishMenuAnimator.gameObject.SetActive(false);
    }
    //Called at every frame
    function Update()
    {
        //If the game is in play mode
        if (inPlayMode)
        {
            //Update the main UI's status texts
            coinText.text = AddDigitDisplay(collectedCoins, 4);
            distanceText.text = AddDigitDisplay(distanceTraveled, 5);
        }
    }
    //Called, when the player clicks on the top menu arrow button
    public function ChangeTopMenuState(arrowImage : Image)
    {
        //If the top menu is in the default position
        if (!topMenuAnimator.GetBool("MoveDown"))
        {
            //Change the button sprite, and move the menu down
            arrowImage.sprite = arrowSprites[1];
            topMenuAnimator.SetBool("MoveDown", true);
            overlayAnimator.SetBool("Visible", true);
        }
        else
        {
            //If the top menu is visible, but but mission menu is not
            if (!missionMenuAnimator.GetBool("ShowMissions"))
            {
                //Hide the top menu
                overlayAnimator.SetBool("Visible", false);
                topMenuAnimator.SetBool("MoveDown", false);
                arrowImage.sprite = arrowSprites[0];
            }
            //If the top menu is visible, and the mission menu is visible as well
            else
            {
                //Hide the mission menu
                missionMenuAnimator.SetBool("ShowMissions", false);
            }
        }
    }
    //Called, when the player clicks on an audio button. Change audio state (enabled, disabled)
    public function ChangeAudioState()
    {
        //Change the state, and update the button sprites
        AudioManagerJS.instance.ChangeAudioState();
        UpdateAudioButtons();
    }
    //Called when the player click on a shop button
    public function ToggleShopMenu()
    {
        //Make sure that the shop is activated
        shopAnimator.gameObject.SetActive(true);

        //If the shop panel is visible
        if (shopAnimator.GetBool("ShowPanel"))
        {
            //Hide and disable it
            shopAnimator.SetBool("ShowPanel", false);
            StartCoroutine(DisableMenu(shopAnimator, 0.5f));
        }
            //If the shop menu is hidden
        else
        {
            //Update the shop, and move it to the view
            UpdateShopDisplay();
            shopAnimator.SetBool("ShowPanel", true);
        }
    }
    //Called when the player click on the top menu's mission button
    public function ToggleMissionMenu()
    {
        //Change mission menu state
        missionMenuAnimator.SetBool("ShowMissions", !missionMenuAnimator.GetBool("ShowMissions"));

        //Update mission display
        var missionTexts : String[] = missionManager.GetMissionTexts();
        var missionStats : String[] = missionManager.GetMissionStats();

        for (var i : int = 0; i < 3; i++)
        {
            missionPanelElements[i].Find("Mission Text").GetComponent(Text).text = missionTexts[i];
            missionPanelElements[i].Find("Status Text").GetComponent(Text).text = missionStats[i];
        }
    }
    //Called, when the player buys an extra speed powerup
    public function BuySpeed(priceTag : Text)
    {
        //Obtain price from the pricetag text
        var price : int = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManagerJS.coinAmmount - price >= 0)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManagerJS.coinAmmount -= price;
            SaveManagerJS.extraSpeed += 1;
            SaveManagerJS.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("ExtraSpeed");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys a shield powerup
    public function BuyShield(priceTag : Text)
    {
        //Obtain price from the pricetag text
        var price : int = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManagerJS.coinAmmount - price >= 0)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManagerJS.coinAmmount -= price;
            SaveManagerJS.shield += 1;
            SaveManagerJS.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("Shield");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys a sonic blast powerup
    public function BuySonicBlast(priceTag : Text)
    {
        //Obtain price from the pricetag text
        var price : int = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManagerJS.coinAmmount - price >= 0)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManagerJS.coinAmmount -= price;
            SaveManagerJS.sonicWave += 1;
            SaveManagerJS.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("SonicBlast");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys a revive powerup
    public function BuyRevive(priceTag : Text)
    {
        //Obtain price from the pricetag text
        var price : int = int.Parse(priceTag.text);

        //If the player can purchase the powerup
        if (SaveManagerJS.coinAmmount - price >= 0)
        {
            //Decrease coin ammount, and increase powerup count
            SaveManagerJS.coinAmmount -= price;
            SaveManagerJS.revive += 1;
            SaveManagerJS.SaveData();

            //Notify mission manager, and update shop display
            missionManager.ShopEvent("Revive");
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys the yellow submarine
    public function BuySubmarine1()
    {
        //Change the current skin ID
        SaveManagerJS.currentSkinID = 0;
        SaveManagerJS.SaveData();

        //Update the player, and the shop display
        playerManager.ChangeSkin(0);
        UpdateShopDisplay();
    }
    //Called, when the player buys the green submarine
    public function BuySubmarine2(priceTag : Text)
    {
        //If the submarine is not yet owned
        if (SaveManagerJS.skin2Unlocked == 0)
        {
            //Obtain the price from the pricetag text
            var skin2Price : int = int.Parse(priceTag.text);

            //If the player can purchase the submarine
            if (SaveManagerJS.coinAmmount - skin2Price >= 0)
            {
                //Decrease coin ammount, and unlock the green submarine
                SaveManagerJS.coinAmmount -= skin2Price;
                SaveManagerJS.skin2Unlocked = 1;
                SaveManagerJS.currentSkinID = 1;
                SaveManagerJS.SaveData();

                ////Update the player, and the shop display
                playerManager.ChangeSkin(1);
                UpdateShopDisplay();
            }
        }
        //If the player already owns the submarine
        else if (SaveManagerJS.currentSkinID != 1)
        {
            //Change the current skin ID
            SaveManagerJS.currentSkinID = 1;
            SaveManagerJS.SaveData();

            //Update the player, and the shop display
            playerManager.ChangeSkin(1);
            UpdateShopDisplay();
        }
    }
    //Called, when the player buys the red submarine
	public function BuySubmarine3(priceTag : Text)
	{
		//If the submarine is not yet owned
		if (SaveManagerJS.skin3Unlocked == 0)
		{
			//Obtain the price from the pricetag text
			var skin3Price : int = int.Parse(priceTag.text);
			
			//If the player can purchase the submarine
			if (SaveManagerJS.coinAmmount - skin3Price >= 0)
			{
				//Decrease coin ammount, and unlock the green submarine
				SaveManagerJS.coinAmmount -= skin3Price;
				SaveManagerJS.skin3Unlocked = 1;
				SaveManagerJS.currentSkinID = 2;
				SaveManagerJS.SaveData();
				
				////Update the player, and the shop display
				playerManager.ChangeSkin(2);
				UpdateShopDisplay();
			}
		}
		//If the player already owns the submarine
		else if (SaveManagerJS.currentSkinID != 2)
		{
			//Change the current skin ID
			SaveManagerJS.currentSkinID = 2;
			SaveManagerJS.SaveData();
			
			//Update the player, and the shop display
			playerManager.ChangeSkin(2);
			UpdateShopDisplay();
		}
	}
    //Called, when the player click on the PlayTrigger
    public function PlayTrigger(arrowImage : Image)
    {
        //If the game is not in play mode
        if (!inPlayMode)
        {
            //Set the game to play mode
            inPlayMode = true;
            mainUI.SetActive(true);

            //Hide the main menu
            arrowImage.sprite = arrowSprites[0];
            topMenuAnimator.SetBool("Hide", true);
            missionMenuAnimator.SetBool("ShowMissions", false);
            overlayAnimator.SetBool("Visible", false);

            //Start the level
            levelManager.StartLevel();

            //Show the available powerups
            var powerupCount = [SaveManagerJS.extraSpeed, SaveManagerJS.shield, SaveManagerJS.sonicWave];

            for (var i : int = 0; i < powerupCount.Length; i++)
                if (powerupCount[i] > 0)
                    powerupButtons[i].SetBool("Visible", true);

            StartCoroutine(DisableMenu(topMenuAnimator, 1));
        }
    }
    //Called, when the playe clicks on the pause button
    public function PauseButton()
    {
    	if (!canPause)
			return;

		canPause = false;
        pauseMenuAnimator.gameObject.SetActive(true);

        //If the game is paused
        if (pauseMenuAnimator.GetBool("Visible") == true)
        {
            //Hide the pause menu, and activate the main UI
            overlayAnimator.SetBool("Visible", false);
            pauseMenuAnimator.SetBool("Visible", false);
            mainUI.gameObject.SetActive(true);

            //Show the available powerups
            var powerupCount = [SaveManagerJS.extraSpeed, SaveManagerJS.shield, SaveManagerJS.sonicWave];

            for (var i : int = 0; i < powerupCount.Length; i++)
                if (powerupCount[i] > 0)
                    powerupButtons[i].SetBool("Visible", true);

            //Resume the game
            levelManager.ResumeLevel();
            StartCoroutine(DisableMenu(pauseMenuAnimator, 1));
        }
        //If the game is not paused, and can be paused
        else if (powerupManager.CanUsePowerup())
        {
            // Show the pause menu, and disable the main UI
            overlayAnimator.SetBool("Visible", true);
            pauseMenuAnimator.SetBool("Visible", true);
            mainUI.gameObject.SetActive(false);

            //Update pause menu mission texts
            var missionTexts : String[] = missionManager.GetMissionTexts();
            var missionStats : String[] = missionManager.GetMissionStats();

            for (var j : int = 3; j < 6; j++)
            {
                missionPanelElements[j].Find("Mission Text").GetComponent(Text).text = missionTexts[j - 3];
                missionPanelElements[j].Find("Status Text").GetComponent(Text).text = missionStats[j - 3];
            }

            //Pause the game
            levelManager.PauseLevel();
        }
        
        StartCoroutine (EnablePause());
    }
    //Called, when the player clicks on a retry button
    public function Retry()
    {
        //Hide the menus
        overlayAnimator.SetBool("Visible", false);

        if (pauseMenuAnimator.gameObject.activeSelf)
        {
            pauseMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(pauseMenuAnimator, 1));
        }

        if (finishMenuAnimator.gameObject.activeSelf)
        {
            finishMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(finishMenuAnimator, 1));
        }
        
        //Reset the game
        powerupManager.Reset();
        levelManager.Restart();

        //Reset the coin ammount
        collectedCoins = 0;

        //Activate the main UI
        mainUI.gameObject.SetActive(true);

        //Show available powerup
        var powerupCount = [SaveManagerJS.extraSpeed, SaveManagerJS.shield, SaveManagerJS.sonicWave];

        for (var i : int = 0; i < powerupCount.Length; i++)
            if (powerupCount[i] > 0)
                powerupButtons[i].SetBool("Visible", true);
    }
    //Called, when the player clicks on a quit button
    public function QuitToMain()
    {
        //Disable menus
        overlayAnimator.SetBool("Visible", false);
        
        if (pauseMenuAnimator.gameObject.activeSelf)
        {
            pauseMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(pauseMenuAnimator, 1));
        }

        if (finishMenuAnimator.gameObject.activeSelf)
        {
            finishMenuAnimator.SetBool("Visible", false);
            StartCoroutine(DisableMenu(finishMenuAnimator, 1));
        }

        //Show top menu
        topMenuAnimator.gameObject.SetActive(true);
        topMenuAnimator.SetBool("MoveDown", false);
        topMenuAnimator.SetBool("Hide", false);

        //Reset the coin ammount
        collectedCoins = 0;

        //Reset powerups, and quit to main menu
		powerupManager.Reset();
        levelManager.QuitToMain();
        inPlayMode = false;
    }
    //Receive current distance
    public function UpdateDistance(newDist : int)
    {
        distanceTraveled = newDist;
    }
    //Receive collected coins ammount
    public function UpdateCoins(newCoins : int)
    {
        collectedCoins = newCoins;
    }
    //Called, when the player collider with a powerup
    public function ShowPowerup(name : String)
    {
        //Increase powerup count, and show powerup icon based on the name of the powerup
        switch (name)
        {
            case "ExtraSpeed":
                SaveManagerJS.extraSpeed += 1;
                powerupButtons[0].SetBool("Visible", true);
                break;

            case "Shield":
                SaveManagerJS.shield += 1;
                powerupButtons[1].SetBool("Visible", true);
                break;

            case "SonicBlast":
                SaveManagerJS.sonicWave += 1;
                powerupButtons[2].SetBool("Visible", true);
                break;

            case "Revive":
                SaveManagerJS.revive += 1;
                break;
        }
    }
    //Called, when the player activates a powerup
    public function HidePowerup(anim : Animator)
    {
        //If a powerup can't be activated, return
        if (!powerupManager.CanUsePowerup())
            return;

        //Play powerup sound
        AudioManagerJS.instance.PlayPowerupUsed();

        //Remove a powerup, and activate it's effect, based on it's name
        switch (anim.gameObject.name)
        {
            case "Speed Button":
                SaveManagerJS.extraSpeed -= 1;
                powerupManager.ExtraSpeed();
                break;

            case "Shield Button":
                SaveManagerJS.shield -= 1;
                powerupManager.Shield();
                break;

            case "Sonic Wave Button":
                SaveManagerJS.sonicWave -= 1;
                powerupManager.SonicBlast();
                break;
        }

        //Save changes, and hide the powerup button
        SaveManagerJS.SaveData();
        anim.SetBool("Visible", false);
    }
    //Called, when the player activates the revive powerup
    public function RevivePlayer()
    {
        //Remove the used revive
        SaveManagerJS.revive -= 1;
        SaveManagerJS.SaveData();

        //Revive the player
        AudioManagerJS.instance.PlayRevive();
        powerupManager.Revive();
        levelManager.ReviveUsed();
        StopCoroutine("Revive");

        //Hide revive button
        powerupButtons[3].SetBool("Visible", false);
    }
    //Called, after the player has crashed
    public function ShowCrashScreen(distance : int)
    {
        //If the player has a revive, show it
        if (SaveManagerJS.revive > 0)
            StartCoroutine("Revive");
        //Else, show the finish menu
        else
            ShowFinishMenu();
    }
    //Called, when a mission is completed
    public function ShowMissionComplete(text : String)
    {
        //Find the first unused mission notificator, and show it
        for (var i : int = 0; i < 3; i++)
        {
            if (!usedMissionNotifications[i])
            {
                usedMissionNotifications[i] = true;

                missionNotifications[i].transform.Find("Text").GetComponent(Text).text = text;
                StartCoroutine(MissionNotificationCountdown(missionNotifications[i], "Pos" + (i + 1), i));

                return;
            }
        }
    }
    //Updates the sprite of the audio buttons
    private function UpdateAudioButtons()
    {
        var s : Sprite = AudioManagerJS.instance.audioEnabled == true ? audioSprites[0] : audioSprites[1];

        for (var item : Image in audioButtons)
            item.sprite = s;
    }
    //Updates the shop display texts
    private function UpdateShopDisplay()
    {
        //Update texts
        coinAmmount.text = SaveManagerJS.coinAmmount.ToString();

        ShopOwnedItems[0].text = SaveManagerJS.extraSpeed.ToString();
        ShopOwnedItems[1].text = SaveManagerJS.shield.ToString();
        ShopOwnedItems[2].text = SaveManagerJS.sonicWave.ToString();
        ShopOwnedItems[3].text = SaveManagerJS.revive.ToString();

        //If the yellow submarine is active
        if (SaveManagerJS.currentSkinID == 0)
        {
            //Set button sprite
            shopSubmarineButtons[0].sprite = ShopSkinButtonSprites[0];

            //Set sprite for button 2
            if (SaveManagerJS.skin2Unlocked == 1)
                shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[1];
            else
                shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[2];

			//Set sprite for button 3
			if (SaveManagerJS.skin3Unlocked == 1)
				shopSubmarineButtons[2].sprite = ShopSkinButtonSprites[1];
			else
				shopSubmarineButtons[2].sprite = ShopSkinButtonSprites[2];
        }
        //If the green submarine is active
		else if (SaveManagerJS.currentSkinID == 1)
        {
            //Set button sprites
            shopSubmarineButtons[0].sprite = ShopSkinButtonSprites[1];
            shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[0];

			//Set sprite for button 3
			if (SaveManagerJS.skin3Unlocked == 1)
				shopSubmarineButtons[2].sprite = ShopSkinButtonSprites[1];
			else
				shopSubmarineButtons[2].sprite = ShopSkinButtonSprites[2];
        }
		//If the red submarine is active
		else
		{
			//Set button 1 sprites
			shopSubmarineButtons[0].sprite = ShopSkinButtonSprites[1];

			//Set sprite for button 2
			if (SaveManagerJS.skin2Unlocked == 1)
				shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[1];
			else
				shopSubmarineButtons[1].sprite = ShopSkinButtonSprites[2];

			//Set button 3 sprites
			shopSubmarineButtons[2].sprite = ShopSkinButtonSprites[0];
		}
    }
    //Shows the finish menu
    private function ShowFinishMenu()
    {
        //Disable main UI, and show the finish menu
        mainUI.gameObject.SetActive(false);
        overlayAnimator.SetBool("Visible", true);
        finishMenuAnimator.gameObject.SetActive(true);
        finishMenuAnimator.SetBool("Visible", true);

        //Set mission texts
        var missionTexts : String[] = missionManager.GetMissionTexts();
        var missionStats : String[] = missionManager.GetMissionStats();

        for (var i : int = 6; i < 9; i++)
        {
            missionPanelElements[i].Find("Mission Text").GetComponent(Text).text = missionTexts[i - 6];
            missionPanelElements[i].Find("Status Text").GetComponent(Text).text = missionStats[i - 6];
        }

        //Set distance and coin text
        finishDistanceText.text = distanceTraveled + "M";
        finishCoinText.text = collectedCoins.ToString();

        //Add collected coins to total coins
        levelManager.LevelEnded();
        hangarDistanceText.text = SaveManagerJS.bestDistance + " M";

        collectedCoins = 0;
    }
    //Returns true, if the game is in play mode
    public function InPlayMode()
    {
        return inPlayMode;
    }
    //Converts a number to a string, with a given digit number. For example, this turns 4 to "0004"
    private function AddDigitDisplay(number : int, digits : int)
    {
        var s : String = "";

        for (var i : int = 0; i < digits - number.ToString().Length; i++)
            s += "0";

        s += number.ToString();

        return s;
    }
    //Shows a mission notificator for 2 seconds, then hides it
    private function MissionNotificationCountdown(missionNotification : Animator, boolName : String, arrayID : int)
    {
        missionNotification.SetBool(boolName, true);
        yield WaitForSeconds(2);
        missionNotification.SetBool(boolName, false);
        usedMissionNotifications[arrayID] = false;
    }
    //Shows the revive button for 3 seconds
    //#pragma warning disable
    public function Revive()
    {
        powerupButtons[3].SetBool("Visible", true);
        yield WaitForSeconds(3);

        powerupButtons[3].SetBool("Visible", false);
        ShowFinishMenu();
    }
        //Disables a specific menu after a given time
    private function DisableMenu(menu : Animator, time : float)
    {
        yield WaitForSeconds(time);
        menu.gameObject.SetActive(false);
    }
    //Enables the interaction with the pause menu
	private function EnablePause()
	{
		yield WaitForSeconds(1f);
		canPause = true;
	}
}