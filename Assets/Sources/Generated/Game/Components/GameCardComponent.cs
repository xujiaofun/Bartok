//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Bartok.CardComponent card { get { return (Bartok.CardComponent)GetComponent(GameComponentsLookup.Card); } }
    public bool hasCard { get { return HasComponent(GameComponentsLookup.Card); } }

    public void AddCard(string newName, string newSuit, int newRank, Bartok.CardDefinition newDef, UnityEngine.Color newColor, string newColS) {
        var index = GameComponentsLookup.Card;
        var component = CreateComponent<Bartok.CardComponent>(index);
        component.name = newName;
        component.suit = newSuit;
        component.rank = newRank;
        component.def = newDef;
        component.color = newColor;
        component.colS = newColS;
        AddComponent(index, component);
    }

    public void ReplaceCard(string newName, string newSuit, int newRank, Bartok.CardDefinition newDef, UnityEngine.Color newColor, string newColS) {
        var index = GameComponentsLookup.Card;
        var component = CreateComponent<Bartok.CardComponent>(index);
        component.name = newName;
        component.suit = newSuit;
        component.rank = newRank;
        component.def = newDef;
        component.color = newColor;
        component.colS = newColS;
        ReplaceComponent(index, component);
    }

    public void RemoveCard() {
        RemoveComponent(GameComponentsLookup.Card);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCard;

    public static Entitas.IMatcher<GameEntity> Card {
        get {
            if (_matcherCard == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Card);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCard = matcher;
            }

            return _matcherCard;
        }
    }
}
