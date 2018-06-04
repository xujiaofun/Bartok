using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Bartok
{
    public sealed class GameStartSystem : IInitializeSystem
    {
        GameContext game;

        public GameStartSystem(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            InitResMgr();
            ReadDeck();
            MakeCards();
        }

        void InitResMgr() {
            var prefab = Resources.Load<GameObject>("DeckResMgr");
            var resMgr = prefab.GetComponent<DeckResMgr>();
            this.game.SetDeckResMgr(resMgr);
        }

        void ReadDeck() {
            var txt = Resources.Load<TextAsset>("DeckXML");  // 此处不需要后缀

            var xmlr = new PT_XMLReader();
            xmlr.Parse(txt.text);

            var decorators = new List<Decorator>();
            var xDecos = xmlr.xml["xml"][0]["decorator"];
            for (int i = 0; i < xDecos.length; i++)
            {
                var deco = new Decorator();
                deco.type = xDecos[i].att("type");
                deco.flip = xDecos[i].att("filp") == "1";
                deco.scale = float.Parse(xDecos[i].att("scale"));
                deco.loc.x = float.Parse(xDecos[i].att("x"));
                deco.loc.y = float.Parse(xDecos[i].att("y"));
                deco.loc.z = float.Parse(xDecos[i].att("z"));
                decorators.Add(deco);
            }

            var cardDefs = new List<CardDefinition>();
            var xCardDefs = xmlr.xml["xml"][0]["card"];
            for (int i = 0; i < xCardDefs.length; i++)
            {
                var cardDef = new CardDefinition();
                cardDef.rank = int.Parse(xCardDefs[i].att("rank"));

                cardDef.pips = new List<Decorator>();
                var xPips = xCardDefs[i]["pip"];
                if (xPips != null)
                {
                    for (int j = 0; j < xPips.length; j++)
                    {
                        var deco = new Decorator();
                        deco.type = "pip";
                        deco.flip = xPips[j].att("flip") == "1";
                        deco.loc.x = float.Parse(xPips[j].att("x"));
                        deco.loc.y = float.Parse(xPips[j].att("y"));
                        deco.loc.z = float.Parse(xPips[j].att("z"));

                        if (xPips[j].HasAtt("scale"))
                        {
                            deco.scale = float.Parse(xPips[j].att("scale"));
                        }
                        cardDef.pips.Add(deco);
                    }
                }

                if (xCardDefs[i].HasAtt("face"))
                {
                    cardDef.face = xCardDefs[i].att("face");
                }
                cardDefs.Add(cardDef);
            }

            this.game.SetDeck(decorators, cardDefs);
        }

        void MakeCards() {
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

                var e = this.game.CreateEntity();
                e.AddCard(cardNames[i], suit, rank, cardDef, color, colS);
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