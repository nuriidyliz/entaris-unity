/***
 * This script will anchor a GameObject to a relative screen position.
 * This script is intended to be used with CameraFit.cs by Marcel Căşvan, available here: http://gamedev.stackexchange.com/a/89973/50623
 * 
 * Note: For performance reasons it's currently assumed that the game resolution will not change after the game starts.
 * You could not make this assumption by periodically calling UpdateAnchor() in the Update() function or a coroutine, but is left as an exercise to the reader.
 */
/* The MIT License (MIT)
 
Copyright (c) 2015, Eliot Lash
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */
using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class CameraAnchor : MonoBehaviour
{
	public enum AnchorType
	{
		Undefined,
		BottomLeft,
		BottomCenter,
		BottomRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		TopLeft,
		TopCenter,
		TopRight,
	};

	private Vector3 standardPosition;
	public AnchorType anchorType;
	public Vector3 anchorOffset;

	// Use this for initialization
	void Start()
	{
#if UNITY_EDITOR
		if (!EditorApplication.isPlaying && anchorType == AnchorType.Undefined)
		{
			standardPosition = transform.position;
		}
		else
		{
			Vector3 newPos = GetAnchorVector() + anchorOffset;
			standardPosition = transform.position - newPos;
		}

#endif
#if !UNITY_EDITOR
        Vector3 newPos = GetAnchorVector() + anchorOffset;
        standardPosition = transform.position - newPos;
#endif


		UpdateAnchor();
	}

	Vector3 GetAnchorVector()
	{
		Vector3 anchor = Vector3.zero;
		switch (anchorType)
		{
			case AnchorType.BottomLeft:
				anchor = CameraFit.Instance.BottomLeft;
				break;
			case AnchorType.BottomCenter:
				anchor = CameraFit.Instance.BottomCenter;
				break;
			case AnchorType.BottomRight:
				anchor = CameraFit.Instance.BottomRight;
				break;
			case AnchorType.MiddleLeft:
				anchor = CameraFit.Instance.MiddleLeft;
				break;
			case AnchorType.MiddleCenter:
				anchor = CameraFit.Instance.MiddleCenter;
				break;
			case AnchorType.MiddleRight:
				anchor = CameraFit.Instance.MiddleRight;
				break;
			case AnchorType.TopLeft:
				anchor = CameraFit.Instance.TopLeft;
				break;
			case AnchorType.TopCenter:
				anchor = CameraFit.Instance.TopCenter;
				break;
			case AnchorType.TopRight:
				anchor = CameraFit.Instance.TopRight;
				break;
			case AnchorType.Undefined:
				anchor = Vector3.zero;
				break;
		}
		return anchor;
	}

	void UpdateAnchor()
	{
		Vector3 anchorVector = GetAnchorVector();
		SetAnchor(anchorVector);
	}

	void SetAnchor(Vector3 anchor)
	{
		Vector3 newPos = anchor + anchorOffset;
		if (!transform.position.Equals(standardPosition + newPos))
		{
			transform.position = standardPosition + newPos;
		}
	}


	// Update is called once per frame
#if UNITY_EDITOR
	void Update()
	{
		if (!EditorApplication.isPlaying)
			UpdateAnchor();


	}
#endif
}