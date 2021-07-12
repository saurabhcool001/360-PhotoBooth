using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShutterScript : MonoBehaviour 
{
	private int numberOfShots;

	private float shotDelay;
	public AudioSource shutterSound;

	public InputField _text_noOfShots;
	public InputField _text_shotDelay;

	void Start()
	{
		
	}

	void Update()
	{
		numberOfShots = int.Parse (_text_noOfShots.text);
		shotDelay = float.Parse (_text_shotDelay.text);
	}

	IEnumerator StartShutter(bool go)
	{
		if (go) 
		{
			for (int i = 0; i < numberOfShots; i++) 
				{
					yield return new WaitForSeconds (shotDelay);
					StartCoroutine (ScreenshotManager.Save ("MyImage", "360 Photo Booth"));
					shutterSound.Play ();
					
				}

			yield return new WaitForSeconds (1);
			GameObject.FindGameObjectWithTag ("MainCanvas").GetComponent<Canvas> ().enabled = true;
		}
	}


	public void TakeMultipleShots()
	{
		StartCoroutine (StartShutter (true));

	}

}
