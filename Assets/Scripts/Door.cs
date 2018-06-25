using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public int Number = 0;
	public GameObject Locked;
	public GameObject Check;
	public GameObject Crystal;
	public GameObject Fruit;

	public Sprite cry;
	public Sprite fru;

	void Start() {
		LevelStat stats = LevelStat.ReadInfo("Level" + Number);
		if (stats == null) 
			stats = new LevelStat();
		
		if (Number == 1 || (LevelStat.ReadInfo("Level" + (Number - 1)).levelPassed)) {
			
		}
		else {
			EnableLock();
		}
		
		if (LevelStat.ReadInfo("Level" + Number).levelPassed)
		{
			EnableCheck();
			if (LevelStat.ReadInfo("Level" + Number).hasAllFruits) {
				EnableFruit();
			}
			else {
				GameObject obj2 = GameObject.Instantiate(this.Crystal);
				obj2.transform.position = new Vector3(this.transform.position.x-0.2f, this.transform.position.y+1.15f, .0f);
			}

			if (LevelStat.ReadInfo("Level" + Number).hasCrystals) {
				EnableCrystal();
			}
			else {
				GameObject obj3 = GameObject.Instantiate(this.Fruit);
				obj3.transform.position = new Vector3(this.transform.position.x+0.15f, this.transform.position.y+1.15f, .0f);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
		if (rabbit != null && Locked) {
			SceneManager.LoadScene("Level" + Number);
		}
	}

	void EnableLock() {
		Destroy(Locked);GameObject obj1 = GameObject.Instantiate(this.Locked);
		obj1.transform.position = new Vector3(this.transform.position.x-0.5f, this.transform.position.y-0.1f, .0f);
	}

	void EnableCheck() {
		GameObject obj = GameObject.Instantiate(this.Check);
		obj.transform.position = new Vector3(this.transform.position.x-0.65f, this.transform.position.y+2.35f, .0f);
	}

	void EnableCrystal() {
		SpriteRenderer sr = Crystal.GetComponent<SpriteRenderer>();
		sr.sprite = cry;
	}

	void EnableFruit() {
		SpriteRenderer sr = Fruit.GetComponent<SpriteRenderer>();
		sr.sprite = fru;
	}
}
