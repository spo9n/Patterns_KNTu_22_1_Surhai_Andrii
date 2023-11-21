namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer
{
    public class Logger : IObserver
    {
        public void Update(string message)
        {
            // Код для запису події у файл (наприклад, використовуючи System.IO)
            string logMessage = $"{DateTime.Now}: {message}";

            // Запис у файл
            File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }
    }
}
