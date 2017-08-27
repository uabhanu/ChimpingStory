using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    void Start()
    {
        AllPlayerPrefs(); //This entire Class & it's object in scene is for testing purposes only
    }

    public static void AllPlayerPrefs()
    {
		PlayerPrefs.DeleteAll();
	}
}
