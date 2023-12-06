namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Memento
{
    public class InstrumentCaretaker
    {
        public Stack<InstrumentMemento> Mementos { get; private set; }


        public InstrumentCaretaker()
        {
            Mementos = new Stack<InstrumentMemento>();
        }

        public void AddMemento(InstrumentMemento memento)
        {
            Mementos.Push(memento);
        }

        public InstrumentMemento GetMemento()
        {
            return Mementos.Pop();
        }
    }
}
