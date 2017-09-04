﻿using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
	AudioSource[] _audiSource;
	
	void Start()
    {
		_audiSource = GetComponents<AudioSource>();
	}
	
	public void SoundToPlay(string type)
    {
		switch(type)
        {
	        case "Jump":
		        _audiSource[0].Play();
	        break;

	        case "Pickup":
		        _audiSource[1].Play();
	        break;

	        case "EnemyDeath":
		        _audiSource[2].Play();
	        break;

	        case "restart":
		        _audiSource[3].Play();
	        break;
		}
	}
}
