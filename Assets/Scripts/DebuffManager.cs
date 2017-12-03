using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour {

	[SerializeField]
	private bool applyAmmoDamageDebuff = true;
	public bool AmmoDamageDebuffActive {
		get {
			return applyAmmoDamageDebuff;
		}
	}

	[SerializeField]
	private bool applyHealthFireCooldownDebuff = true;
	public bool HealthFireCooldownDebuffActive {
		get {
			return applyAmmoDamageDebuff;
		}
	}

}
