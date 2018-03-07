using UnityEngine;
using System.Collections;

public class CameraShake_Quadrant : CameraShake
{
	private Vector3 convertVec;
	
	public override void Shake(ShakeParam param)
	{
		if (param != null)
		{
			if (param.shakeType == CameraShakeType.eCST_Quadrant || param.shakeType == CameraShakeType.eCST_All)
			{
				Reset (true, param.shakeDistanceScale);
				
				Vector3 objForward = param.transParam.forward;
				convertVec = Quaternion.Inverse(parentTrans.rotation) * objForward;
				
				Vector3 vecYZ = Vector3.forward + Vector3.up;
				vecYZ.Normalize();
				Vector3 vexX = Vector3.right;
				if (convertVec.x > 0 && convertVec.y <= 0)
				{
					vecYZ *= -1.0f;
				}
				else if (convertVec.x <= 0 && convertVec.y < 0)
				{
					vecYZ *= -1.0f;
					vexX *= -1.0f;
				}
				else if (convertVec.x < 0 && convertVec.y >= 0)
				{
					vexX *= -1.0f;
				}
				convertVec = vecYZ + vexX;
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