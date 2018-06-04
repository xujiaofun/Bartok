using Entitas;

namespace Bartok
{
    /// <summary>
    /// 矿工接龙
    /// </summary>
    public sealed class ProspectorSystems : Feature
    {
        public ProspectorSystems(Contexts contexts)
            : base("ProspectorSystems")
        {
            this.Add(new GameStartSystem(contexts));

            this.Add(new CreateGameBoardSystem(contexts));

            this.Add(new ProcessTouchSystem(contexts));

            this.Add(new AddViewSystem(contexts));
            this.Add(new RenderPositionSystem(contexts));
        }
    }
}