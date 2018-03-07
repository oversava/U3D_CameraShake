using UnityEngine;
using System.Collections;

public enum CameraShakeType
{
	eCST_None,
	eCST_All, // this is for debug
	eCST_ForBackward,
	eCST_UpDown,
	eCST_UpDown_Global,
	eCST_Horizontal,
	eCST_MUL_FB_Hor,
	eCST_Quadrant,
}

public class ShakeParam
{
	public Transform transParam = null;
	public CameraShakeType shakeType = CameraShakeType.eCST_None;
	public float shakeDistanceScale = 1.0f;
}

public abstract class CameraShake : MonoBehaviour
{
	public AnimationCurve MoveAnmCurve;
	public float ShakeTime;
	public float ShakeMaxDistance;

	protected float shakeDistanceScale;

	protected Transform parentTrans;
	protected Transform cameraTrans;
	protected bool isShaking;
	protected float beginTime;
	protected float lastTime;

	void Start()
	{
		cameraTrans = transform;
		parentTrans = cameraTrans.parent;
		Reset (false);
	}

	protected void Reset(bool state, float disScale = 1.0f)
	{
//		if (isShaking && state)
//		{
//			return;
//		}

        if (cameraTrans == null)
            cameraTrans = transform;

		shakeDistanceScale = disScale;

		cameraTrans.localPosition = Vector3.zero;
		cameraTrans.localEulerAngles = Vector3.zero;

		isShaking = state;
		beginTime = Time.time;
		lastTime = 0.0f;
	}

	protected float GetValueInCurve(float timeFactor)
	{
		return MoveAnmCurve.Evaluate (lastTime);
	}

	protected bool NextValue(ref float newValue)
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
		int allKey = MoveAnmCurve.keys.Length;
		if (allKey > 0)
		{
			float allTime = MoveAnmCurve.keys [allKey - 1].time;
			newValue = MoveAnmCurve.Evaluate (allTime * lastTime/ShakeTime);
		}

		return true;
	}

	public abstract void Shake(ShakeParam param);
}
