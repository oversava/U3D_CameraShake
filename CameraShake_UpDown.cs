using UnityEngine;
using System.Collections;

public class CameraShake_UpDown : CameraShake
{
	public override void Shake(ShakeParam param)
	{
		if (param.shakeType == CameraShakeType.eCST_UpDown || param.shakeType == CameraShakeType.eCST_All)
		{
			Reset (true, param.shakeDistanceScale);
		}
		else
		{
			Reset (false);
		}
	}
	
	void LateUpdate()
	{
		float newDistance = 0;
		if (!NextValue(ref newDistance))
		{
			return;
		}
		
		// special
		cameraTrans.localPosition = Vector3.up * newDistance * ShakeMaxDistance * shakeDistanceScale;
	}
}