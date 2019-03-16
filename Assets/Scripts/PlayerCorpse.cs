using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCorpse : MonoBehaviour {
    public GameObject bloodSprite;

    public float maxBloodSize;
    float currentBloodSize;

    public void Awake() {
        StartCoroutine("ScaleBlood");
    }

    IEnumerator ScaleBlood() {
        while (currentBloodSize < maxBloodSize) {
            bloodSprite.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 5;

            yield return null;
        }

        yield return null;
    }
}
