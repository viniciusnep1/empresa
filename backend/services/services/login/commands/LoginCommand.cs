using core.commands;

namespace services.commands.command.seguranca
{
    public class LoginCommand : Command
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
