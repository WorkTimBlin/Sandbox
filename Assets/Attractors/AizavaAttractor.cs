using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AizavaAttractor : MonoBehaviour, ISpeedByPositionCalculator
{
	public MonoBehaviour parameterChangeListener;
	public float a = 0.95f;
	public float b = 0.7f;
	public float c = 0.6f;
	public float d = 3.5f;
	public float e = 0.25f;
	public float f = 0.1f;


	IParameterChangeListener ParameterChangeListener =>
		(IParameterChangeListener)parameterChangeListener;
	public Vector3 CalculateSpeed(Vector3 p)
	{
		return new Vector3(
			(p.z - b)*p.x - d*p.y,
			d*p.x + (p.z-b)*p.y,
			c + a*p.z - p.z*p.z*p.z/3 - p.x*p.x + f * p.z * p.x*p.x*p.x);
	}

	void ValidateInterfaces()
	{
		parameterChangeListener =
			parameterChangeListener?.GetComponent<IParameterChangeListener>()
			as MonoBehaviour;
	}

	private void OnValidate()
	{
		ValidateInterfaces();
		ParameterChangeListener?.OnParameterChange();
	}
}
