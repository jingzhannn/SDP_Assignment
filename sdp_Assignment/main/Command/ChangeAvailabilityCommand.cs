using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Command
{
    // updates availability of a MenuItem
    internal class ChangeAvailabilityCommand : ICommand
    {
        private MenuItem item;
        private bool available;

        //concrete
        public ChangeAvailabilityCommand(MenuItem item, bool available)
        {
            this.item = item;
            this.available = available;
        }

        public void Execute()
        {
            // Calls MenuItem's public SetAvailability
            item.SetAvailability(available); //flips T/F
            // console
            Console.WriteLine($"{item.Name} is now {(available ? "Available" : "Not Available")}");
        }
    }
}
