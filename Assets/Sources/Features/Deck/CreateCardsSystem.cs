using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Bartok
{
    public sealed class CreateCardsSystem : IInitializeSystem
    {
        GameContext game;

        public CreateCardsSystem(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            var anchorGO = GameObject.Find("_Deck");
            if (anchorGO == null)
            {
                anchorGO = new GameObject("_Deck");
            }
            var deckAnchor = anchorGO.transform;

            var resMgr = this.game.deckResMgr.value;
            var dictSuits = new Dictionary<string, Sprite>()
            {
                {"C", resMgr.suitClub},
                {"D", resMgr.suitDiamond},
                {"H", resMgr.suitHeart},
                {"S", resMgr.suitSpade}
            };

            var cardNames = new List<string>();
            string[] letters = new string[]{ "C", "D", "H", "S" };
            foreach (var s in letters)
            {
                for (int i = 0; i < 13; i++)
                {
                    cardNames.Add(s + (i+1));
                }
            }
            List<GameEntity> cards = new List<GameEntity>();

            for (int i = 0; i < cardNames.Count; i++)
            {
                var cgo = GameObject.Instantiate(resMgr.prefabCard);
                cgo.transform.parent = deckAnchor;
                cgo.transform.localPosition = new Vector3( (i%13) * 3, i/13*4, 0 ) * 1000;

                var suit = cardNames[i][0].ToString();
                var rank = int.Parse(cardNames[i].Substring(1));
                var cardDef = this.GetCardDefinitionByRank(rank);
                var color = Color.black;
                var colS = "Black";
                if (suit == "D" || suit == "H")
                {
                    colS = "Red";
                    color = Color.red;
                }

                var decoGOs = new List<GameObject>();
                GameObject tGO;
                SpriteRenderer tSR;
                foreach (var deco in this.game.deck.decotators)
                {
                    if (deco.type == "suit")
                    {
                        tGO = GameObject.Instantiate(resMgr.prefabSprite);
                        tSR = tGO.GetComponent<SpriteRenderer>();
                        tSR.sprite = dictSuits[suit];
                    }
                    else
                    {
                        tGO = GameObject.Instantiate(resMgr.prefabSprite);
                        tSR = tGO.GetComponent<SpriteRenderer>();
                        tSR.sprite = resMgr.rankSprites[rank];
                        tSR.color = color;
                    }

                    tSR.sortingOrder = 1;
                    tSR.flipX = deco.flip;

                    tGO.transform.parent = cgo.transform;
                    tGO.transform.localPosition = deco.loc;
                    tGO.transform.localScale = Vector3.one * deco.scale;
                    tGO.name = deco.type;

                    decoGOs.Add(tGO);
                }

                // 创建中间的花色
                foreach (var pip in cardDef.pips)
                {
                    tGO = GameObject.Instantiate(resMgr.prefabSprite);
                    tSR = tGO.GetComponent<SpriteRenderer>();
                    tSR.sprite = dictSuits[suit];
                    tSR.color = color;
                    tSR.sortingOrder = 1;
                    tSR.flipX = pip.flip;

                    tGO.transform.parent = cgo.transform;
                    tGO.transform.localPosition = pip.loc;
                    tGO.transform.localScale = Vector3.one * pip.scale;
                    tGO.name = pip.type;
                }

                if (cardDef.face != "")
                {
                    tGO = GameObject.Instantiate(resMgr.prefabSprite);
                    tSR = tGO.GetComponent<SpriteRenderer>();
                    tSR.sprite = this.GetFace(cardDef.face + suit);
                    tSR.sortingOrder = 1;
                    tGO.transform.parent = cgo.transform;
                    tGO.transform.localPosition = Vector3.zero;
                    tGO.transform.localScale = Vector3.one;
                    tGO.name = "pip";
                }

                var e = this.game.CreateEntity();
                e.AddCard(cardNames[i], suit, rank, cardDef, decoGOs, color, colS);
                e.AddGameObject(cgo);
                cards.Add(e);
            }

            this.game.SetCardCache(cards);
        }

        private CardDefinition GetCardDefinitionByRank(int rnk) {
            foreach (var item in this.game.deck.cardDefs)
            {
                if (item.rank == rnk)
                {
                    return item;
                }
            }
            return null;
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