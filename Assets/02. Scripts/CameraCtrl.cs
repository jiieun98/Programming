// ------------------------------------
// CameraCtrl.cs
//
// 카메라
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    float velocity;

    public GameObject target;
    public Vector3 offset;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

	void Update()
	{
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector3 targetDirection = (target.transform.position - posNoZ);

			velocity = targetDirection.magnitude * 5f;

			targetPos = transform.position + (targetDirection.normalized * velocity * Time.deltaTime);

			transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

		}
	}
}
