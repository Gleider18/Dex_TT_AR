using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

	private Transform _playerCamera;
	public Transform portal;
	public Transform otherPortal;
	
	void Update () {
		// Вычисление смещения игрока от другого портала
		Vector3 playerOffsetFromPortal = _playerCamera.position - otherPortal.position;
		transform.position = portal.position + playerOffsetFromPortal;

		// Вычисление разницы углов между порталами
		Quaternion portalRotationalDifference = Quaternion.Inverse(otherPortal.rotation) * portal.rotation;

		// Применение разницы углов к направлению камеры игрока
		Vector3 newCameraDirection = portalRotationalDifference * _playerCamera.forward;
		Vector3 newCameraUp = portalRotationalDifference * _playerCamera.up;

		// Установка нового направления и вверхнего вектора камеры
		transform.rotation = Quaternion.LookRotation(newCameraDirection, newCameraUp);
	}

	public void Initialize(Transform player) => _playerCamera = player;
}
