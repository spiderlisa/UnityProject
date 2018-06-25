using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

	public void ShowNumber() {
		Text count = GameObject.Find("CoinCount").GetComponent<Text>();
		count.text = Coins.n.ToString();
	}
}
