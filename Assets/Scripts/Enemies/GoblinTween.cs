using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GoblinTween : MonoBehaviour
{
    GameObject go;
    bool animate = true;

    private void Awake()
    {
        go = gameObject;
    }
    void Start()
    {
        StartTween();
    }

    public void StartTween()
    {
        animate = true;
        StartCoroutine("GoblinAnimate");
    }
    public void StopTween()
    {
        animate = false;
    }

    public IEnumerator GoblinAnimate()
    {
        while (animate)
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
                "time", animate ? 0.25f : 0.1f,
                "easeType", iTween.EaseType.easeInSine,
                "isLocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, animate ? -10 : 0),
                "time", animate ? 0.25f : 0.1f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.3f);
        }

    }
}
