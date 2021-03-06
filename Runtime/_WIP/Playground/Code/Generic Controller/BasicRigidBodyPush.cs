
#if UNITY

namespace Lost
{ 
	using UnityEngine;

	public class BasicRigidBodyPush : MonoBehaviour
	{
		#pragma warning disable 0649
		[SerializeField] private LayerMask pushLayers;
		[SerializeField] private bool canPush;

		[Range(0.5f, 5f)]
		[SerializeField] private float strength = 1.1f;
		#pragma warning restore 0649

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (this.canPush) 
			{
				this.PushRigidBodies(hit);
			}
		}

		private void PushRigidBodies(ControllerColliderHit hit)
		{
			// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

			// Make sure we hit a non kinematic rigidbody
			Rigidbody body = hit.collider.attachedRigidbody;
			
			if (body == null || body.isKinematic)
			{ 
				return;
			}

			// Make sure we only push desired layer(s)
			var bodyLayerMask = 1 << body.gameObject.layer;
			if ((bodyLayerMask & pushLayers.value) == 0)
			{
				return;
			}

			// We dont want to push objects below us
			if (hit.moveDirection.y < -0.3f)
			{ 
				return;
			}

			// Calculate push direction from move direction, horizontal motion only
			Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

			// Apply the push and take strength into account
			body.AddForce(pushDir * this.strength, ForceMode.Impulse);
		}
	}
}

#endif
