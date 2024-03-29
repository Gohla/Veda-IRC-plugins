﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.Core
{
    [Plugin(Name = "Core", Description = "Provides several core functionality such as help and echoing.")]
    public static class CorePlugin
    {
        [Command(Description = "Lists all plugins.")]
        public static IEnumerable<String> List(IContext context)
        {
            IEnumerable<String> pluginNames = context.Bot.Plugin.Plugins
                .Select(p => p.Name)
                ;

            context.Seperator = " ";

            return pluginNames;
        }

        [Command(Description = "Lists all command in a plugin.")]
        public static IEnumerable<String> List(IContext context, IPlugin plugin)
        {
            IEnumerable<String> commandNames = context.Bot.Command.GetCommands(plugin)
                .Select(p => p.Name.ToLower())
                .Distinct()
                ;

            context.Seperator = ", ";

            return commandNames;
        }

        [Command(Description = "Shows information about a command.")]
        public static IEnumerable<String> Help(IContext context, IEnumerable<ICommand> command)
        {
            return command
                .Select(c => c.ToString() + " -- " + c.Description)
                ;
        }

        [Command(Description = "Shows information about a plugin.")]
        public static String Help(IContext context, IPlugin plugin)
        {
            return plugin.Description;
        }

        [Command(Description = "Shows information about a command in a plugin.")]
        public static IEnumerable<String> Help(IContext context, IPlugin plugin, String command)
        {
            IEnumerable<ICommand> candidates = context.Bot.Command.GetCommands(plugin.Name, command);

            if(candidates.IsEmpty())
                throw new ArgumentException("Command " + command + " not found in " + plugin.Name + ".");

            return candidates
                .Select(c => c.ToString() + " -- " + c.Description)
                ;
        }

        [Command(Description = "Returns more messages from the buffer of the previous command.")]
        public static String More(IContext context)
        {
            context.ReplyForm = ReplyForm.More;
            return context.Bot.More();
        }

        [Command(Description = "Returns more messages from the buffer of the previous command from given user.")]
        public static String More(IContext context, IUser user)
        {
            context.ReplyForm = ReplyForm.More;
            return context.Bot.More(user);
        }

        [Command(Description = "Echoes message to the channel the command was sent to, or to the user that sent the command.")]
        public static String Echo(IContext context, String message)
        {
            context.ReplyForm = ReplyForm.Echo;
            return message;
        }

        [Command(Description = "Replies to the user that sent this command.")]
        public static String Reply(IContext context, String reply)
        {
            context.ReplyForm = ReplyForm.Reply;
            return reply;
        }

        [Command(Description = "Sends an action message to the channel the command was sent to, or to the user that sent the command.")]
        public static String Action(IContext context, String action)
        {
            context.ReplyForm = ReplyForm.Action;
            return action;
        }

        [Command(Description = "Sends a notice to the channel the command was sent to, or to the user that sent the command.")]
        public static String Notice(IContext context, String notice)
        {
            context.ReplyForm = ReplyForm.Notice;
            return notice;
        }
    }
}
