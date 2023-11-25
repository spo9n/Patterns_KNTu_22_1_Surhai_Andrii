namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer
{
    public interface IDAOObservable
    {
        void AddObserver(IObserver observer);

        void RemoveObserver(IObserver observer);

        void Notify(string message);
    }
}
