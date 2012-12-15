using System;
using System.Collections.Generic;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.List
{
    [Plugin(Name = "List", Description = "Created, modifies and retrieves lists.")]
    public class ListPlugin
    {
        [Command(Description = "Creates a global list with given identifier.")]
        public void Create(IContext context, String identifier)
        {
            IStorage storage = context.Bot.Storage.Global(context.Command.Plugin);
            List<object> list = storage.Get<List<object>>(identifier);
            if(list != null)
                throw new ArgumentException("List with given identifier already exists globally.", "identifier");
            storage.Set(identifier, new List<object>());
        }

        [Command(Description = "Creates a server-scoped list on given server with given identifier. Server parameter may be omitted to select the current server.")]
        public void Create(IContext context, IClientConnection server, String identifier)
        {
            IStorage storage = context.Bot.Storage.Server(context.Command.Plugin, server);
            List<object> list = storage.Get<List<object>>(identifier);
            if(list != null)
                throw new ArgumentException("List with given identifier already exists for given server.", "identifier");
            storage.Set(identifier, new List<object>());
        }

        [Command(Description = "Creates a channel-scoped list on given channel with given identifier. Channel parameter may be omitted to select the current channel.")]
        public void Create(IContext context, IChannel channel, String identifier)
        {
            IStorage storage = context.Bot.Storage.Channel(context.Command.Plugin, channel);
            List<object> list = storage.Get<List<object>>(identifier);
            if(list != null)
                throw new ArgumentException("List with given identifier already exists for given channel.", "identifier");
            storage.Set(identifier, new List<object>());
        }

        [Command(Description = "Gets item from a list at given index.")]
        public object Get(IContext context, String identifier, int index)
        {
            List<object> list = GetListThrows(context, identifier);
            return list[index];
        }

        [Command(Description = "Adds given item to a list.")]
        public void Add(IContext context, String identifier, object item)
        {
            List<object> list = GetListThrows(context, identifier);
            list.Add(item);
        }

        [Command(Description = "Removes given item from a list.")]
        public void Remove(IContext context, String identifier, object item)
        {
            List<object> list = GetListThrows(context, identifier);
            list.Remove(item);
        }

        [Command(Description = "Removes item at given index from a list.")]
        public void RemoveAt(IContext context, String identifier, int index)
        {
            List<object> list = GetListThrows(context, identifier);
            list.RemoveAt(index);
        }

        [Command(Description = "Clears a list.")]
        public void Clear(IContext context, String identifier)
        {
            List<object> list = GetListThrows(context, identifier);
            list.Clear();
        }

        [Command(Description = "Gets the size of a list.")]
        public int Size(IContext context, String identifier)
        {
            List<object> list = GetListThrows(context, identifier);
            return list.Count;
        }

        private List<object> GetListThrows(IContext context, String identifier)
        {
            List<object> list = context.Storage.Get<List<object>>(identifier);
            if(list == null)
                throw new ArgumentException("List with given identifier does not exist.", "identifier");
            return list;
        }
    }
}
