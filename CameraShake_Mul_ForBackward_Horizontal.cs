using UnityEngine;
using System.Collections;

public class CameraShake_Mul_ForBackward_Horizontal : MonoBehaviour
{
	public AnimationCurve HorMoveAnmCurve;
	public AnimationCurve FBMoveAnmCurve;
	public float ShakeTime;
	public float HorShakeMaxDistance;
	public float FBShakeMaxDistance;

	protected float HorShakeDistanceScale;
	protected float FBShakeDistanceScale;

	protected Transform parentTrans;
	protected Transform cameraTrans;
	protected bool isShaking;
	protected float beginTime;
	protected float lastTime;

	private Vector3 convertVec;
	
	void Start()
	{
		cameraTrans = transform;
		parentTrans = cameraTrans.parent;
		Reset (false);
	}

	void LateUpdate()
	{
		float newHorDistance = 0;
		float newFBDistance = 0;
		if (!NextValue(ref newHorDistance, ref newFBDistance))
		{
			return;
		}
		
		// special
		cameraTrans.localPosition = convertVec * newHorDistance * HorShakeMaxDistance * HorShakeDistanceScale;
		cameraTrans.localPosition += Vector3.forward * newFBDistance * FBShakeMaxDistance * FBShakeDistanceScale;
	}
	
	private void Reset(bool state, float HorDisScale = 1.0f, float FBDisScale = 1.0f)
	{
		HorShakeDistanceScale = HorDisScale;
		FBShakeDistanceScale = FBDisScale;

		cameraTrans.localPosition = Vector3.zero;
		cameraTrans.localEulerAngles = Vector3.zero;
		
		isShaking = state;
		beginTime = Time.time;
		lastTime = 0.0f;
	}
	
	private bool NextValue(ref float HorNewValue, ref float FBNewValue )
	{
		if (!isShaking)
		{
			return false;
		}
		
		lastTime = Time.time - beginTime;
		if (lastTime >= ShakeTime)
		{
			Reset(false);
			return false;
		}
		int allKey = HorMoveAnmCurve.keys.Length;
		if (allKey > 0)
		{
			float allTime = HorMoveAnmCurve.keys [allKey - 1].time;
			HorNewValue = HorMoveAnmCurve.Evaluate (allTime * lastTime/ShakeTime);
		}
		allKey = FBMoveAnmCurve.keys.Length;
		if (allKey > 0)
		{
			float allTime = FBMoveAnmCurve.keys [allKey - 1].time;
			FBNewValue = FBMoveAnmCurve.Evaluate (allTime * lastTime/ShakeTime);
		}
		
		return true;
	}
	
	public void Shake(ShakeParam param)
	{
		if (param != null)
		{
			if (param.shakeType == CameraShakeType.eCST_MUL_FB_Hor || param.shakeType == CameraShakeType.eCST_All)
			{
				Reset (true, param.shakeDistanceScale, param.shakeDistanceScale);
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
}