using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceCameraScript : MonoBehaviour 
{
	private bool camAvailable;
	private WebCamTexture backCam;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;


	public void Start()
	{
		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0) 
		{
			Debug.Log ("No Camera Detected");
			camAvailable = false;
			return;
		}


		for (int i = 0; i < devices.Length; i++) 
		{
			if(devices[i].isFrontFacing)
			{
				backCam = new WebCamTexture (devices [i].name, 2560, 1440, 30);

			}
		}

		if (backCam == null)
		{
			Debug.Log ("Unable to find back Camera");
			return;
		}

		backCam.Play ();
		background.texture = backCam;

		camAvailable = true;
	}



	void Update()
	{
		if (!camAvailable)
			return;

		float ratio = (float)backCam.width / (float)backCam.height;
		fit.aspectRatio = ratio;

		float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
		background.rectTransform.localScale = new Vector3 (1f, -1f, 1f);

		int orient = -backCam.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3 (0, 0, orient);
	}
}
