// The MIT License (MIT)
//
// Copyright (c) 2015, Shakhruz Ashirov.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.

using UnityEngine;
using System.Collections;

// Place this script on an emtpy game object and make it parent for CardboardMain
// it will rotate this gameobject around target object when user is turning his/her head around
public class OrbitAroundTarget : MonoBehaviour {
	public Transform target;
	public float rotationSpeed = 1.0f;
	public bool reverseDirection = false;
	public float distance = 2f;
	public float verticalDistance = -0.5f;
	public bool verticalRotation = true;
	public float blidSpot = 0.05f;
	public float maxArea = 0.6f;
	
	private float angleY, angleX, angleDiffY, angleDiffX;
	
	private Vector3 offset;
	
	void Start () {
		offset = new Vector3 (0, verticalDistance, distance);
	}
	
	void LateUpdate()
	{
		angleY = Cardboard.SDK.HeadPose.Orientation.y;
		angleX = Cardboard.SDK.HeadPose.Orientation.x;
		angleDiffX = -Mathf.Clamp(angleX, -maxArea, maxArea);
		angleDiffY = Mathf.Clamp(angleY, -maxArea, maxArea);
		if (!reverseDirection) {
			angleDiffX = - angleDiffX;
			angleDiffY = - angleDiffY;
		}
		
		if (Mathf.Abs(angleDiffX) >= maxArea || Mathf.Abs(angleDiffX) < blidSpot) angleDiffX = 0;
		if (Mathf.Abs(angleDiffY) >= maxArea || Mathf.Abs(angleDiffY) < blidSpot) angleDiffY = 0;
		
		if (verticalRotation) {
			offset = Quaternion.AngleAxis (angleDiffX * rotationSpeed, Vector3.right) * 
				Quaternion.AngleAxis (angleDiffY * rotationSpeed, Vector3.up) * 
				offset;
			transform.position = target.position + offset;
		} else {
			offset = Quaternion.AngleAxis (angleDiffY * rotationSpeed, Vector3.up) * offset;
			transform.position = target.position + offset; 
		}
		
		transform.LookAt(target.position);
	}
}
