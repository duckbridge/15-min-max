using UnityEngine;
using System.Collections;

public class MathUtils {

	public static Vector3 CalculateDirection(Vector3 target, Vector3 origin) {
		Vector3 direction = target - origin;
		direction.Normalize();
		return direction;
	}

	public static bool IsWithinBounds(Bounds checkedBounds, Bounds withinBounds) {
		return checkedBounds.Intersects(withinBounds);
	}
}
