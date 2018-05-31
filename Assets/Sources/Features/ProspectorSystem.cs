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
            this.Add(new CreateDeckSystem(contexts));

            this.Add(new CreateCardsSystem(contexts));
        }
    }
}