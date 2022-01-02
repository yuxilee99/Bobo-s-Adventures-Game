using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(this.gameObject);
            SoundManager.S.MakeCoinSound();
            GameManager.S.UpdateScore(10);
        }
    }
}
