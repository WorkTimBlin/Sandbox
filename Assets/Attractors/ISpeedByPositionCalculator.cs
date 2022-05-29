using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedByPositionCalculator
{
	Vector3 CalculateSpeed(Vector3 position);
}
