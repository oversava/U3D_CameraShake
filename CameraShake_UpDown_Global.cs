using UnityEngine;
using System.Collections;

public class CameraShake_UpDown_Global : CameraShake
{
	public override void Shake(ShakeParam param)
	{
		if (param.shakeType == CameraShakeType.eCST_UpDown_Global || param.shakeType == CameraShakeType.eCST_All)
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
		Vector3 globalPos = parentTrans.position;
		float newY = globalPos.y + newDistance * ShakeMaxDistance * shakeDistanceScale;
		cameraTrans.position = new Vector3 (globalPos.x, newY, globalPos.z);
	}
}