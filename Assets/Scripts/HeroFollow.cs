using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabbit Rabbit;

	void Update () {
		Transform rabbitTransform = Rabbit.transform;
		Transform cameraTransform = this.transform;

		Vector3 rabbitPosition = rabbitTransform.position;
		Vector3 cameraPosition = cameraTransform.position;

		cameraPosition.x = rabbitPosition.x;
		cameraPosition.y = rabbitPosition.y+2f;

		cameraTransform.position = cameraPosition;
	}
}
