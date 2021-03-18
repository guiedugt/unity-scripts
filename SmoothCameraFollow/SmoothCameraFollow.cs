using UnityEngine;

namespace EvertaleGames
{
	[DisallowMultipleComponent]
	public class SmoothCameraFollow : MonoBehaviour {

		[SerializeField] Transform target;
		[SerializeField] float smoothSpeed = 10f;
		[SerializeField] Vector3 offset = new Vector3(0, 2, -10);

		Vector3 velocity = Vector3.zero;

		void Start () {
			if ( !target ) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}
		}

		void FixedUpdate () {
			Vector3 desiredPosition = target.position + offset;
			Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.fixedDeltaTime);
			transform.position = smoothPosition;
			transform.LookAt(target);
		}	
	}
}
