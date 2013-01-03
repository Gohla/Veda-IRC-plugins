using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.IRC
{
    [Plugin(Name = "IRC", Description = "Miscellaneous IRC functionality.")]
    public static class IRCPlugin
    {
        [Command(Description = "Retrieves your identity.")]
        public static IIdentity Identity(IContext context)
        {
            return context.Sender.Identity;
        }

        [Command(Description = "Retrieves the identity of given user.")]
        public static IIdentity Identity(IContext context, IUser user)
        {
            return user.Identity;
        }

        [Command(Description = "Tries to match given identity to given identity mask.")]
        public static bool IdentityMatch(IContext context, IIdentity identity, IdentityMask mask)
        {
            return mask.Match(identity);
        }
    }
}
