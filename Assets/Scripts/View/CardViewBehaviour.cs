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

        this.entity.OnComponentReplaced += Entity_OnComponentReplaced;
    }

    void Entity_OnComponentReplaced (IEntity e, int index, IComponent previousComponent, IComponent newComponent)
    {
        if (index == GameComponentsLookup.FaceUp) {
            FaceUp(entity.faceUp.value);
        }
        else if (index == GameComponentsLookup.SortOrder) {
            SetSortingOrder(entity.sortOrder.value);
        }
        else if (index == GameComponentsLookup.CardProspector) {
            SetSortingLayerName(entity.cardProspector.slotDef.layerName);
        }
    }

    public void FaceUp(bool b)
    {
        this.back.SetActive(!b);
    }

    void PopulateSpriteRenderers() {
        if (spriteRenders == null || spriteRenders.Length == 0)
        {
            spriteRenders = GetComponentsInChildren<SpriteRenderer>();
        }
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

    void OnMouseDown() {
        var e = game.CreateEntity();
        e.AddTouch(entity);
    }

}

