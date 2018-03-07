using UnityEngine;
using System.Collections;

public class CameraShake_Horizontal : CameraShake
{
	private Vector3 convertVec;

	public override void Shake(ShakeParam param)
	{
		if (param != null)
		{
			if (param.shakeType == CameraShakeType.eCST_Horizontal || param.shakeType == CameraShakeType.eCST_All)
			{
				Reset (true, param.shakeDistanceScale);
				Vector3 objForward = param.transParam.forward;
				convertVec = Quaternion.Inverse(parentTrans.rotation) * objForward;
				convertVec.Normalize();
			}
			else
			{
				Reset (false);
			}
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
		cameraTrans.localPosition = convertVec * newDistance * ShakeMaxDistance * shakeDistanceScale;
	}
}