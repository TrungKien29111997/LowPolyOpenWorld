namespace Core.Gameplay
{
    public interface IPlayerBehavious : IState<Player>
    {
        void OnFixUpdate(Player player);
    }
}