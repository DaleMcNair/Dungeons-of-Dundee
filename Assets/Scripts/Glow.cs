using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Glow : MonoBehaviour
{
    public Shader glowShader;
    public SpriteRenderer sprite;
    public Material material;

    private void Awake()
    {
        Debug.Log(sprite);
    }

}
