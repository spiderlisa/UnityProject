using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : Collectable {
    public Sprite FullHeart;

    protected override void OnRabbitHit(HeroRabbit rabbit) {
        this.CollectedHide();
        if (LevelController.lives < 3) {
            Image crystImg = GameObject.Find("Heart" + ++LevelController.lives).GetComponent<Image>();
            crystImg.sprite = FullHeart;
        }
    }
}
