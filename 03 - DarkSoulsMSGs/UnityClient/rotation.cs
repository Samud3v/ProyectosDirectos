using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{

	[SerializeField]
	float frequency = 5f;

	[SerializeField]
	float magnitude = 5f;

	[SerializeField]
	float velRotacion = 1f;


	Vector3 pos, localScale;

	// Use this for initialization
	void Start()
	{

		pos = transform.position;

		localScale = transform.localScale;

	}

	// Update is called once per frame
	void Update()
	{
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, velRotacion * Time.deltaTime,0));
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

}
