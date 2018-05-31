using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Bartok
{
    public sealed class CreateDeckSystem : IInitializeSystem
    {
        GameContext game;

        public CreateDeckSystem(Contexts contexts)
        {
            game = contexts.game;
        }

        public void Initialize()
        {
            var txt = Resources.Load<TextAsset>("DeckXML");  // 此处不需要后缀
            Debug.Log(txt.text);

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

            var deckEntity = this.game.CreateEntity();
            deckEntity.AddBartokDeck(decorators, cardDefs);

            var prefab = Resources.Load<GameObject>("DeckResMgr");
            var resMgr = prefab.GetComponent<DeckResMgr>();
            deckEntity.AddBartokDeckResMgr(resMgr);
        }
    }
}