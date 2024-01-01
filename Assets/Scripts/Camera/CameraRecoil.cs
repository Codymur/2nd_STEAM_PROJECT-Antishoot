using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
	[Header("Recoil Settings")]
	public float rotationSpeed = 6;
	public float returnSpeed = 25;
	[Space()]

	[Header("Hipfire:")]
	public Vector3 RecoilRotation = new Vector3(2f, 2f, 2f);
	[Space()]

	[Header("Aiming")]
	public Vector3 RecoilRotationAiming = new Vector3(0.5f, 0.5f, 1.5f);
	[Space()]

	private Vector3 currentRotation;
	private Vector3 Rot;
	private void FixedUpdate()
	{
		currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
		Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
		transform.localRotation = Quaternion.Euler(Rot);
	}

	public void Fire()
	{
		rotationSpeed = 6f;
		returnSpeed = 15f;
		RecoilRotation = new Vector3(30f, 20f, 7f);
		currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
	}
	public void FireUzi()
	{
		rotationSpeed = 12f;
		returnSpeed = 20f;
		RecoilRotation = new Vector3(15f, 10f, 3.5f);
		currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
	}
	public void FirePowerful()
	{
		rotationSpeed = 2f;
		returnSpeed = 7f;
		RecoilRotation = new Vector3(75f, 20f, 7f);
		currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
	}
}
