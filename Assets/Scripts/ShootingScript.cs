using UnityEngine;
using UnityEngine.Networking;

public class ShootingScript : NetworkBehaviour
{
	public ParticleSystem _muzzleFlash;
	public AudioSource _gunAudio;
	public GameObject _impactPrefab;
	public GameObject _bulletPrefab;
	public Transform cameraTransform;
	public Transform bulletSpawn;

	ParticleSystem _impactEffect;


	void Start ()
	{
		_impactEffect = Instantiate (_impactPrefab).GetComponent<ParticleSystem> ();
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			
			_muzzleFlash.Stop ();
			_muzzleFlash.Play ();
			CmdFire ();
			_gunAudio.Stop ();
			_gunAudio.Play ();

			RaycastHit hit;
			Vector3 rayPos = cameraTransform.position + (1f * cameraTransform.forward);

			if (Physics.Raycast (rayPos, cameraTransform.forward, out hit, 50f)) {
				_impactEffect.transform.position = hit.point;
				_impactEffect.Stop ();
				_impactEffect.Play ();

				if (hit.transform.tag == "Player") {
					CmdHitPlayer (hit.transform.gameObject);
				}
			}
		}
	}

	[Command]
	void CmdFire ()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			             _bulletPrefab,
			             bulletSpawn.position,
			             bulletSpawn.rotation);
		NetworkServer.Spawn (bullet);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 600;

		// Destroy the bullet after 2 seconds
		Destroy (bullet, 1.0f);
	}

	[Command]
	void CmdHitPlayer (GameObject hit)
	{
		hit.GetComponent<NetworkedPlayerScript> ().RpcResolveHit ();
	}
}