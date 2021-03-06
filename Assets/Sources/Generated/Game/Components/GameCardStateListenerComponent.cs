//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CardStateListenerComponent cardStateListener { get { return (CardStateListenerComponent)GetComponent(GameComponentsLookup.CardStateListener); } }
    public bool hasCardStateListener { get { return HasComponent(GameComponentsLookup.CardStateListener); } }

    public void AddCardStateListener(System.Collections.Generic.List<ICardStateListener> newValue) {
        var index = GameComponentsLookup.CardStateListener;
        var component = CreateComponent<CardStateListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCardStateListener(System.Collections.Generic.List<ICardStateListener> newValue) {
        var index = GameComponentsLookup.CardStateListener;
        var component = CreateComponent<CardStateListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCardStateListener() {
        RemoveComponent(GameComponentsLookup.CardStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherCardStateListener;

    public static Entitas.IMatcher<GameEntity> CardStateListener {
        get {
            if (_matcherCardStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CardStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCardStateListener = matcher;
            }

            return _matcherCardStateListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddCardStateListener(ICardStateListener value) {
        var listeners = hasCardStateListener
            ? cardStateListener.value
            : new System.Collections.Generic.List<ICardStateListener>();
        listeners.Add(value);
        ReplaceCardStateListener(listeners);
    }

    public void RemoveCardStateListener(ICardStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = cardStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveCardStateListener();
        } else {
            ReplaceCardStateListener(listeners);
        }
    }
}
