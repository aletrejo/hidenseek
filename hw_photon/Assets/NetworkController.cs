using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		if (photonView.isMine)
//			HandleMovement ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Space pressed.");
			ForceJump (new Vector3(3,3,3));
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo msg){
		if (stream.isWriting)
			stream.SendNext(GetComponent<Rigidbody>().position);
		else
			GetComponent<Rigidbody>().position = (Vector3)stream.ReceiveNext();
	}

	[PunRPC] void ForceJump(Vector3 dir){
		Debug.Log (dir);
		GetComponent<PhotonView> ().RequestOwnership ();
		transform.position = dir;
		//GetComponent<Rigidbody>().addForce(dir);

		if(photonView.isMine)
			photonView.RPC("ForceJump", PhotonTargets.Others, dir);
	}
}
