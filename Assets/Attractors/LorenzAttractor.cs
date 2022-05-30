using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LorenzAttractor : MonoBehaviour, ISpeedByPositionCalculator
{
	public MonoBehaviour parameterChangeListener;
	public Vector3 sigmaRoBeta;

	IParameterChangeListener ParameterChangeListener =>
		(IParameterChangeListener)parameterChangeListener;
	public Vector3 CalculateSpeed(Vector3 position)
	{
		return new Vector3(
			sigmaRoBeta.x * (position.y - position.x),
			position.x * (sigmaRoBeta.y - position.z) - position.y,
			position.x * position.y - sigmaRoBeta.z * position.z);
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
