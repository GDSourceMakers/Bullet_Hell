using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class Letterboxing : MonoBehaviour
{
	const float KEEP_ASPECT = 6 / 8f;

	public Camera cameraForm;

	void Start()
	{
		/*
		float aspectRatio = Screen.width / ((float)Screen.height);
		float percentage = (aspectRatio / KEEP_ASPECT);

		cameraForm.rect = new Rect((percentage / 2),0f, (1 - percentage),1f);
		*/

		// CameraAspectRatio
		// By Nicolas Varchavsky @ Interatica (www.interatica.com)
		// Date: March 27th, 2009
		// Version: 1.0

		// Attach this script to all the cameras you want to modify the aspect ratio.


		// here we setup the targeted aspect ratio for width and height
		float targetAspectWidth = 6f;
		float targetAspectHeight = 8f;


		// we'll try to calculate what's the biggest resolution we can accomplish
		// to work in the desired aspect ratio

		// get screen size
		float sw = Screen.width;
		float sh = Screen.height;

		// let's check if we should modify the height first...
		// calculate the targeted size height
		float th = sw * (targetAspectHeight / targetAspectWidth);

		// these variables will hold the percentage of height or width we need to
		// apply to the camera.rect property
		// by default, we set them up in 1.0
		float ptw = 1.0f;
		float pth = 1.0f;

		// these variables will help us adjust the margin to center the screen
		float tx = 0.0f;
		float ty = 0.0f;
		float tw;
		float half = 0.0f;

		// let's try the height...
		// to do this, we check how much the targeted height represents on the screen height
		// so, if the result is greater than one, it means the height should not be modified since
		// the width is the one needing to be adjusted
		pth = th / sh;

		// check if either the height or the width needs to be adjusted
		if (pth > 1.0)
		{
			// since the result was greater than 1.0, we'll work on the width
			// we do the same thing as above, but with the width
			tw = sh * (targetAspectWidth / (float)targetAspectHeight);
			ptw = tw / sw;

			// get half of the percentage we're taking from the width
			half = (1.0f - ptw) / 2.0f;

			// adjust the margin
			tx = half;

		}
		else
		{
			// get half of the percentage we're taking from the height
			half = (1.0f - pth) / 2.0f;

			// adjust the margin
			ty = half;
		}


		// apply the camera.rect   
		Rect r = new Rect();
		r.x = tx;
		r.y = ty;
		r.width = ptw;
		r.height = pth;
		cameraForm.rect = r;
	}
	
}