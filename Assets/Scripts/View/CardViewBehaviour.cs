using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.Unity;

public class CardViewBehaviour : MonoBehaviour
{
    GameContext game;
    GameEntity entity;
    SpriteRenderer[] spriteRenders;
    GameObject back;

    void Awake() {
        var link = this.gameObject.GetEntityLink();
        this.game = link.context as GameContext;
        this.entity = link.entity as GameEntity;

        this.back = this.gameObject.transform.Find("back").gameObject;
    }

    public void FaceUp(bool b)
    {
        this.back.SetActive(b);
    }

    void PopulateSpriteRenderers() {
//        if (spriteRenders == null || spriteRenders.Length == 0)
//        {
            spriteRenders = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log("spriteRenders length=" + spriteRenders.Length);
//        }
    }

    public void SetSortingLayerName(string layerName) {
        PopulateSpriteRenderers(); 
        foreach (var tSR in spriteRenders)
        {
            tSR.sortingLayerName = layerName;
        }
    }

    public void SetSortingOrder(int sOrd) {
        PopulateSpriteRenderers();

        foreach (var tSR in spriteRenders)
        {
            if (tSR.gameObject == this.gameObject)
            {
                tSR.sortingOrder = sOrd;
                continue;
            }

            if (tSR.gameObject.name == "back") {
                tSR.sortingOrder = sOrd + 2;
            } 
            else {
                tSR.sortingOrder = sOrd + 1;
            }
        }
    }

}

