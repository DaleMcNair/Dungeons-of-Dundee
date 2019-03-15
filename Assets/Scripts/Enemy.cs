using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
    Entity entity;

    private void Start() {
        entity = GetComponent<Entity>();
    }


}
