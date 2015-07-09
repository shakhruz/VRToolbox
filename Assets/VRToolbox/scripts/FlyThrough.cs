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
// This script will move player or the gameobject it is placed on through way points with delays, different speed and ability to specify
// where to orient the camera
public class FlyThrough : MonoBehaviour {

	[System.Serializable]
	public class WayPoint {
		public Transform point;
		public float speed = 1.0f;
		public float delay = 0f;
		public Transform lookAt;
	}
	
	public WayPoint[] wayPoints;

	private Transform player;                  
	private int currentPoint = 0;
	private int nextPoint = 1;
	private float startTime;
	private float minDistance = 0.001f;
	private float distance;
	private bool isPaused;
	
	void Awake()
	{
		if (player==null) player = transform;
	}
	
	void Start () {
		currentPoint = 0;
		if (wayPoints.Length > 0) {
			player.position = wayPoints[currentPoint].point.position;
			PrepareForNextPoint();
		}
	}
	
	void Update () {
		if (wayPoints.Length < 2) return;
		if (isPaused) {
			if (Time.time - startTime >= wayPoints[currentPoint].delay) isPaused = false;
			else return;
		}  

		if (Vector3.Distance(player.position, wayPoints[nextPoint].point.position) < minDistance) {
			currentPoint = nextPoint;
			PrepareForNextPoint();
		}
		
		float distCovered = (Time.time - startTime - wayPoints[currentPoint].delay) * wayPoints[currentPoint].speed;
		float fracJourney = distCovered / distance;
		
		player.position = Vector3.Lerp(player.position, wayPoints[nextPoint].point.position, fracJourney);
		if (wayPoints[currentPoint].lookAt != null) player.LookAt(wayPoints[currentPoint].lookAt);
	}
	
	void PrepareForNextPoint()
	{
		nextPoint = currentPoint + 1;
		startTime = Time.time;
		isPaused = true;
		if (nextPoint >= wayPoints.Length) nextPoint = 0;
		distance = Vector3.Distance(player.position, wayPoints[nextPoint].point.position);
		if (wayPoints[currentPoint].lookAt != null) player.LookAt(wayPoints[currentPoint].lookAt);
	}
}
