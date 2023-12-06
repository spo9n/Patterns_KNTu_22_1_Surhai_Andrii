namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer
{
    public class Observer : IObserver
    {
        private string filePath;


        public Observer(string filePath)
        {
            this.filePath = filePath;
        }

        public void Update(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR. {ex}");
            }
        }
    }
}
