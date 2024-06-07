using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
	public static void Log(string msg)
	{
		Debug.Log(Time.time + " : " + msg);
	}

	public static void Log(float msg)
	{
		Debug.Log(Time.time + " : " + msg);
	}

	public static void Log(bool msg)
	{
		Debug.Log(Time.time + " : " + msg.ToString());
	}

	public static int nbRandom(int max)
	{
		return Random.Range(0, max);
	}

	public static bool nbRandomIf()
	{
		if (Random.Range(0, 2) == 0)
		{
			return false;
		}
		return true;
	}

	public static int nbRandomSens()
	{
		if (Random.Range(0, 2) == 0)
		{
			return -1;
		}
		return 1;
	}

	public static int nbRandomSens3()
	{
		switch (Random.Range(0, 3))
		{
		case 0:
			return -1;
		case 1:
			return 1;
		default:
			return 0;
		}
	}

	public static void rendererEnable(GameObject obj, bool enable)
	{
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.GetComponent<Renderer>().enabled = enable;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static void rendererLevel2Enable(GameObject obj, bool enable)
	{
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				IEnumerator enumerator2 = transform.transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Transform transform2 = (Transform)enumerator2.Current;
						transform2.GetComponent<Renderer>().enabled = enable;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = enumerator2 as IDisposable) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = enumerator as IDisposable) != null)
			{
				disposable2.Dispose();
			}
		}
	}

	public static float QualityToV3(Vector3 v)
	{
		// if (Config.Quality == -1)
		// {
		// 	return v.x;
		// }
		// if (Config.Quality == 0)
		// {
		// 	return v.y;
		// }
		return v.z;
	}

	public static void drawOutline(string style, string msg, Rect position, Color colorIn, Color colorOut, int outlineSize, bool lowFps)
	{
		if (!lowFps)
		{
			GUI.skin.GetStyle(style).normal.textColor = colorOut;
			position.x -= 1f;
			position.y -= 1f;
			GUI.Label(position, msg, style);
			position.x += 2f;
			position.y = position.y;
			GUI.Label(position, msg, style);
			position.x = position.x;
			position.y += 2f;
			GUI.Label(position, msg, style);
			position.x -= 2f;
			position.y = position.y;
			GUI.Label(position, msg, style);
			position.x += 1f;
			position.y -= 1f;
			position.x -= outlineSize;
			GUI.Label(position, msg, style);
			position.x += outlineSize * 2;
			GUI.Label(position, msg, style);
			position.x -= outlineSize;
			position.y -= outlineSize;
			GUI.Label(position, msg, style);
			position.y += outlineSize * 2;
			GUI.Label(position, msg, style);
			position.y -= outlineSize;
		}
		GUI.skin.GetStyle(style).normal.textColor = colorIn;
		GUI.Label(position, msg, style);
	}

	public static Vector3 normalizeAngle(Vector3 pos, Vector3 pos2, bool shortAngle)
	{
		float num = pos2.x;
		float num2 = pos2.y;
		float num3 = pos2.z;
		if (shortAngle)
		{
			if (Mathf.Abs(pos2.x - pos.x) > 180f)
			{
				num = ((!(pos2.x - pos.x < 0f)) ? (num - 360f) : (num + 360f));
			}
			if (Mathf.Abs(pos2.y - pos.y) > 180f)
			{
				num2 = ((!(pos2.y - pos.y < 0f)) ? (num2 - 360f) : (num2 + 360f));
			}
			if (Mathf.Abs(pos2.z - pos.z) > 180f)
			{
				num3 = ((!(pos2.z - pos.z < 0f)) ? (num3 - 360f) : (num3 + 360f));
			}
		}
		else
		{
			if (Mathf.Abs(pos2.x - pos.x) < 180f && pos.x != pos2.x)
			{
				num += 360f;
			}
			if (Mathf.Abs(pos2.y - pos.y) < 180f && pos.y != pos2.y)
			{
				num2 += 360f;
			}
			if (Mathf.Abs(pos2.z - pos.z) < 180f && pos.z != pos2.z)
			{
				num3 += 360f;
			}
		}
		return new Vector3(num, num2, num3);
	}

	public static float Angle180(float angle)
	{
		if (angle > 180f)
		{
			return angle - 360f;
		}
		if (angle < -180f)
		{
			return angle + 360f;
		}
		return angle;
	}

	public static bool IsInRange(float x, float start, float end)
	{
		if (x >= start - end && x <= start + end)
		{
			return true;
		}
		return false;
	}

	public static float SmoothFollow(float current, float target, float smoothIncrease)
	{
		if (Mathf.Sign(current) != Mathf.Sign(target))
		{
			current = ((!(current < 0f)) ? (-0.0001f) : 0f);
			return current;
		}
		if (current < target)
		{
			current += smoothIncrease;
			if (current > target)
			{
				return target;
			}
		}
		else if (current > target)
		{
			current -= smoothIncrease;
			if (current < target)
			{
				return target;
			}
		}
		return current;
	}

	public static float LerpFollowStep(float current, float target, float InverseSpeed, float maxSpeedFwd, float maxSpeedBack)
	{
		float num = Mathf.Abs(Mathf.Abs(current) - Mathf.Abs(target)) / InverseSpeed;
		if (Mathf.Approximately(current, target))
		{
			return 0f;
		}
		if (current < target)
		{
			if (num > maxSpeedFwd)
			{
				num = maxSpeedFwd;
			}
			if (current + num > target)
			{
				return target - current;
			}
			return num;
		}
		if (current > target)
		{
			if (num > maxSpeedBack)
			{
				num = maxSpeedBack;
			}
			if (current - num < target)
			{
				return target - current;
			}
			return 0f - num;
		}
		return 0f;
	}

	public static float LerpFollowStepChangeSign(float current, float target, float InverseSpeed, float maxSpeed, float maxSpeedChange)
	{
		float num = Mathf.Abs(Mathf.Abs(current) - Mathf.Abs(target)) / InverseSpeed;
		float num2 = maxSpeed;
		bool flag = false;
		if (!Mathf.Approximately(target, 0f) && Mathf.Sign(current) != Mathf.Sign(target))
		{
			num2 = maxSpeedChange;
			flag = true;
		}
		if (Mathf.Approximately(current, target))
		{
			return 0f;
		}
		if (current < target)
		{
			if (num > num2 || flag)
			{
				num = num2;
			}
			if (current + num > target)
			{
				return target - current;
			}
			return num;
		}
		if (current > target)
		{
			if (num > num2 || flag)
			{
				num = num2;
			}
			if (current - num < target)
			{
				return target - current;
			}
			return 0f - num;
		}
		return 0f;
	}

	public static float DividedFollowAngleStep(float current, float target, float InverseSpeed)
	{
		float num = Mathf.DeltaAngle(current, target);
		if (Mathf.Abs(num) < 0.001f)
		{
			return 0f;
		}
		return num / InverseSpeed;
	}

	public static float DividedFollowStep(float current, float target, float InverseSpeed)
	{
		float num = target - current;
		if (Mathf.Abs(num) < 0.001f)
		{
			return 0f;
		}
		return num / InverseSpeed;
	}

	public static float AngleTarget01(float current, float target, float offset)
	{
		float num = Mathf.Abs(Mathf.DeltaAngle(current, target));
		num = Mathf.Clamp01(num / offset);
		return 1f - num;
	}

	public static Vector3 SmoothVector(Vector3 current, Vector3 target, float InverseSpeed)
	{
		float num = DividedFollowStep(current.x, target.x, InverseSpeed);
		current.x += num;
		num = DividedFollowStep(current.y, target.y, InverseSpeed);
		num = DividedFollowStep(current.z, target.z, InverseSpeed);
		current.z += num;
		return current;
	}

	public static string NbToPos(int nb)
	{
		switch (nb)
		{
		case 0:
			return "0";
		case 1:
			return "1st";
		case 2:
			return "2nd";
		case 3:
			return "3rd";
		default:
			return nb + "th";
		}
	}

	public static bool IntToBool(int i)
	{
		if (i == 0)
		{
			return false;
		}
		return true;
	}

	public static int BoolToInt(bool i)
	{
		if (!i)
		{
			return 0;
		}
		return 1;
	}

	public static Vector2 V3ToV2(Vector3 tmp)
	{
		return new Vector2(tmp.x, tmp.z);
	}

	public static float DistanceFast(Vector3 v1, Vector3 v2)
	{
		return Mathf.Abs(v2.x - v1.x) + Mathf.Abs(v2.z - v1.z);
	}

	public static int MissionGetPosFromId(int id)
	{
		// for (int i = 0; i < Config.AllMissions.Count; i++)
		// {
		// 	if (Config.AllMissions[i].Id == id)
		// 	{
		// 		return i;
		// 	}
		// }
		return 0;
	}

	public static string FormatSeconds(float elapsed)
	{
		int num = (int)(elapsed * 100f);
		int num2 = num / 6000;
		int num3 = num % 6000 / 100;
		return string.Format("{0:00}:{1:00}", num2, num3);
	}

	public static string FormatSpaceNumber(int number)
	{
		NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
		numberFormatInfo.NumberGroupSeparator = " ";
		NumberFormatInfo numberFormatInfo2 = numberFormatInfo;
		return number.ToString("N0", numberFormatInfo2);
	}

	public static Gradient GradientFrom2Color(Color c1, Color c2)
	{
		Gradient gradient = new Gradient();
		GradientColorKey[] array = new GradientColorKey[2];
		GradientAlphaKey[] array2 = new GradientAlphaKey[2];
		array[0].color = c1;
		array[0].time = 0.6f;
		array[1].color = c2;
		array[1].time = 1f;
		array2[0].alpha = 1f;
		array2[0].time = 0f;
		array2[1].alpha = 1f;
		array2[1].time = 1f;
		gradient.SetKeys(array, array2);
		return gradient;
	}

	public static ParticleSystem.MinMaxGradient RandomFrom2Color(Color c1, Color c2)
	{
		return new ParticleSystem.MinMaxGradient(c1, c2);
	}

	public static Color DarkFromColor(Color c1, float coef)
	{
		return new Color(Mathf.Clamp01(c1.r * coef), Mathf.Clamp01(c1.g * coef), Mathf.Clamp01(c1.b * coef));
	}
}
