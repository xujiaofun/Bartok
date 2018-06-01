using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.Unity;

public class CardViewBehaviour : MonoBehaviour
{
    GameContext game;
    GameEntity entity;

    void Awake() {
        var link = this.gameObject.GetEntityLink();
        this.game = link.context as GameContext;
        this.entity = link.entity as GameEntity;

//        this.entity.
    }

}

