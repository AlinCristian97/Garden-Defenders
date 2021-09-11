namespace General.Patterns.Singleton.Interfaces
{
    public interface ISelectionManager
    {
        Defender DefenderToBuild { get; }
        Defender DefenderToSell { get; }

        void SelectDefenderToBuild(Defender defender);
        void SelectDefenderToSell(Defender defender);

        void DeselectDefenderToBuild();
        void DeselectDefenderToSell();
    }
}