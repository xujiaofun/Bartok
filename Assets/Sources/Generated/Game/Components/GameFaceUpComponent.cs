//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Bartok.FaceUpComponent faceUp { get { return (Bartok.FaceUpComponent)GetComponent(GameComponentsLookup.FaceUp); } }
    public bool hasFaceUp { get { return HasComponent(GameComponentsLookup.FaceUp); } }

    public void AddFaceUp(bool newValue) {
        var index = GameComponentsLookup.FaceUp;
        var component = CreateComponent<Bartok.FaceUpComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceFaceUp(bool newValue) {
        var index = GameComponentsLookup.FaceUp;
        var component = CreateComponent<Bartok.FaceUpComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveFaceUp() {
        RemoveComponent(GameComponentsLookup.FaceUp);
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

    static Entitas.IMatcher<GameEntity> _matcherFaceUp;

    public static Entitas.IMatcher<GameEntity> FaceUp {
        get {
            if (_matcherFaceUp == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FaceUp);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFaceUp = matcher;
            }

            return _matcherFaceUp;
        }
    }
}
