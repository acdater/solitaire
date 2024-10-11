using Assets.MainAssets.Scripts.Cards;
using Assets.MainAssets.Scripts.Enums;

namespace Assets.MainAssets.Scripts.Interfaces
{
    public interface IStackable
    {
        void Add(Card card, bool undo = false);
        void Remove(Card card);

        StackRule GetRule();
    }
}
