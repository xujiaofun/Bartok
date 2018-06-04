using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Entitas.Unity;

namespace Bartok
{
    public sealed class AddViewSystem : ReactiveSystem<GameEntity>, IInitializeSystem
    {
        GameContext game;
        Transform layoutAnchor;

        public AddViewSystem(Contexts contexts)
            : base(contexts.game)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            var tGO = GameObject.Find("_LayoutAnchor");
            if (tGO == null)
            {
                tGO = new GameObject("_LayoutAnchor");
                tGO.transform.position = Vector3.zero;
            }
            layoutAnchor = tGO.transform;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Card);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var resMgr = this.game.deckResMgr.value;
            var dictSuits = new Dictionary<string, Sprite>()
            {
                {"C", resMgr.suitClub},
                {"D", resMgr.suitDiamond},
                {"H", resMgr.suitHeart},
                {"S", resMgr.suitSpade}
            };
            foreach (var e in entities)
            {
                var card = e.card;
                var cgo = GameObject.Instantiate(resMgr.prefabCard);
                cgo.transform.parent = layoutAnchor;

                if (e.hasPosition) {
                    cgo.transform.localPosition = e.position.value;
                }

                GameObject tGO;
                SpriteRenderer tSR;
                foreach (var deco in this.game.deck.decotators)
                {
                    if (deco.type == "suit")
                    {
                        tGO = GameObject.Instantiate(resMgr.prefabSprite);
                        tSR = tGO.GetComponent<SpriteRenderer>();
                        tSR.sprite = dictSuits[card.suit];
                    }
                    else
                    {
                        tGO = GameObject.Instantiate(resMgr.prefabSprite);
                        tSR = tGO.GetComponent<SpriteRenderer>();
                        tSR.sprite = resMgr.rankSprites[card.rank];
                        tSR.color = card.color;
                    }

                    tSR.sortingOrder = 1;
                    tSR.flipX = deco.flip;

                    tGO.transform.parent = cgo.transform;
                    tGO.transform.localPosition = deco.loc;
                    tGO.transform.localScale = Vector3.one * deco.scale;
                    tGO.name = deco.type;
                }

                // 创建中间的花色
                foreach (var pip in card.def.pips)
                {
                    tGO = GameObject.Instantiate(resMgr.prefabSprite);
                    tSR = tGO.GetComponent<SpriteRenderer>();
                    tSR.sprite = dictSuits[card.suit];
                    tSR.color = card.color;
                    tSR.sortingOrder = 1;
                    tSR.flipX = pip.flip;

                    tGO.transform.parent = cgo.transform;
                    tGO.transform.localPosition = pip.loc;
                    tGO.transform.localScale = Vector3.one * pip.scale;
                    tGO.name = pip.type;
                }

                // 处理花牌
                if (card.def.face != "")
                {
                    tGO = GameObject.Instantiate(resMgr.prefabSprite);
                    tSR = tGO.GetComponent<SpriteRenderer>();
                    tSR.sprite = this.GetFace(card.def.face + card.suit);
                    tSR.sortingOrder = 1;
                    tGO.transform.parent = cgo.transform;
                    tGO.transform.localPosition = Vector3.zero;
                    tGO.transform.localScale = Vector3.one;
                    tGO.name = "pip";
                }

                // 添加纸牌背景
                tGO = GameObject.Instantiate(resMgr.prefabSprite);
                tSR = tGO.GetComponent<SpriteRenderer>();
                tSR.sprite = resMgr.cardBack;
                tSR.sortingOrder = 2;
                tGO.transform.parent = cgo.transform;
                tGO.transform.localPosition = Vector3.zero;
                tGO.transform.localScale = Vector3.one;
                tGO.name = "back";

                e.AddGameObject(cgo);
                cgo.Link(e, this.game);
                var view = cgo.AddComponent<CardViewBehaviour>();
                view.SetSortingLayerName(e.cardProspector.slotDef.layerName);
                view.SetSortingOrder(e.sortOrder.value);
                view.FaceUp(e.faceUp.value);
            }
        }

        private Sprite GetFace(string faceS) {
            var resMgr = this.game.deckResMgr.value;
            foreach (var ts in resMgr.faceSprites)
            {
                if (ts.name == faceS)
                {
                    return ts;
                }
            }
            return null;
        }
    }
}
