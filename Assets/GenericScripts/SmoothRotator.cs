using UnityEngine;

namespace GenericScripts
{
	public class SmoothRotator
	{
		private float _rotationSpeed;
		private readonly float _angleDiffRotationSlowerCoeff;
		
		public SmoothRotator(float rotationSpeed, float rotationSlower)
		{
			_angleDiffRotationSlowerCoeff = 180 / rotationSlower;
			// rotationSlower - во сколько раз замедлится вращение при максимальной разнице в углах
			_rotationSpeed = rotationSpeed;
		}
		
		public Vector3 SmoothLookAt(float prevRotationY, Vector3 targetRotation, ref float rotationLerpValue)
		{
			var newRotation = Mathf.Atan2(targetRotation.x, targetRotation.z) * Mathf.Rad2Deg;
			var angleDiff = Mathf.Abs(Mathf.DeltaAngle(prevRotationY, newRotation));
			newRotation = Mathf.LerpAngle(prevRotationY, newRotation, rotationLerpValue);
			rotationLerpValue += _rotationSpeed / (angleDiff / _angleDiffRotationSlowerCoeff);
			return new Vector3(0, newRotation);
		}
	}
}