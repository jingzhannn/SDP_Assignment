//delete is not nessacary

namespace sdp_Assignment.main.Command
{
    // Acts as an invoker to store and trigger commands
    internal class SimpleInvoker
    {
        private List<ICommand> commandQueue = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            commandQueue.Add(command);
        }

        public void ExecuteAll()
        {
            foreach (var cmd in commandQueue)
            {
                cmd.Execute();
            }
            commandQueue.Clear();
        }
    }
}
