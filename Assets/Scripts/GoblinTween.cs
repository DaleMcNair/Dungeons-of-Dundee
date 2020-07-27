using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GoblinTween : MonoBehaviour
{
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        go = gameObject;

        StartCoroutine("GoblinAnimate");
    }


    IEnumerator GoblinAnimate()
    {
        while (true)
        {
            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0.25f),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeOutSine,
                "islocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, 5),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.25f);

            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeInSine,
                "isLocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, 10),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.3f);

            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0.25f),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeOutSine,
                "islocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, -5),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.25f);

            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeInSine,
                "isLocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, -10),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.3f);
        }

    }
}
