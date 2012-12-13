using System;
using Veda.Interface;
using System.Collections.Generic;
using ReactiveIRC.Interface;

namespace Veda.Plugins.List
{
    [Plugin(Name = "List", Description = "Created, modifies and retrieves lists.")]
    public class ListPlugin
    {
        [Command(Description = "Creates a global list with given identifier.")]
        public void Create(IContext context, String identifier)
        {
            IStorage storage = context.Bot.StorageManager.Global();
            List<object> list = storage.Get<List<object>>(identifier);
            if(list != null)
                throw new ArgumentException("List with given identifier already exists globally.", "identifier");
            SetList(storage, identifier, new List<object>());
        }

        [Command(Description = "Creates a server-scoped list on given server with given identifier. Server parameter may be omitted to select the current server.")]
        public void Create(IContext context, IClientConnection server, String identifier)
        {
            IStorage storage = context.Bot.StorageManager.Server(server);
            List<object> list = storage.Get<List<object>>(identifier);
            if(list != null)
                throw new ArgumentException("List with given identifier already exists for given server.", "identifier");
            SetList(storage, identifier, new List<object>());
        }

        [Command(Description = "Creates a channel-scoped list on given channel with given identifier. Channel parameter may be omitted to select the current channel.")]
        public void Create(IContext context, IChannel channel, String identifier)
        {
            IStorage storage = context.Bot.StorageManager.Channel(channel);
            List<object> list = storage.Get<List<object>>(identifier);
            if(list != null)
                throw new ArgumentException("List with given identifier already exists for given channel.", "identifier");
            SetList(storage, identifier, new List<object>());
        }

        [Command(Description = "Gets item from a list at given index.")]
        public object Get(IContext context, String identifier, int index)
        {
            StorageScope scope;
            List<object> list = GetListThrows(context, identifier, out scope);
            return list[index];
        }

        [Command(Description = "Adds given item to a list.")]
        public void Add(IContext context, String identifier, object item)
        {
            StorageScope scope;
            List<object> list = GetListThrows(context, identifier, out scope);
            list.Add(item);
            SetList(context, scope, identifier, list); // TODO: Should be cached, very inefficient!
        }

        [Command(Description = "Removes given item from a list.")]
        public void Remove(IContext context, String identifier, object item)
        {
            StorageScope scope;
            List<object> list = GetListThrows(context, identifier, out scope);
            list.Remove(item);
            SetList(context, scope, identifier, list); // TODO: Should be cached, very inefficient!
        }

        [Command(Description = "Removes item at given index from a list.")]
        public void RemoveAt(IContext context, String identifier, int index)
        {
            StorageScope scope;
            List<object> list = GetListThrows(context, identifier, out scope);
            list.RemoveAt(index);
            SetList(context, scope, identifier, list); // TODO: Should be cached, very inefficient!
        }

        [Command(Description = "Clears a list.")]
        public void Clear(IContext context, String identifier)
        {
            StorageScope scope;
            List<object> list = GetListThrows(context, identifier, out scope);
            list.Clear();
            SetList(context, scope, identifier, list); // TODO: Should be cached, very inefficient!
        }

        [Command(Description = "Gets the size of a list.")]
        public int Size(IContext context, String identifier)
        {
            StorageScope scope;
            List<object> list = GetListThrows(context, identifier, out scope);
            return list.Count;
        }

        private List<object> GetListThrows(IContext context, String identifier, out StorageScope scope)
        {
            List<object> list = GetList(context, identifier, out scope);
            if(list == null)
                throw new ArgumentException("List with given identifier does not exist.", "identifier");
            return list;
        }

        private List<object> GetList(IContext context, String identifier, out StorageScope scope)
        {
            if(context.Message.Receiver.Type == MessageTargetType.Channel)
            {
                List<object> list = GetList(GetStorage(context, StorageScope.Channel), identifier);
                if(list != null)
                {
                    scope = StorageScope.Channel;
                    return list;
                }
            }

            {
                List<object> list = GetList(GetStorage(context, StorageScope.Server), identifier);
                if(list != null)
                {
                    scope = StorageScope.Server;
                    return list;
                }
            }

            {
                List<object> list = GetList(GetStorage(context, StorageScope.Global), identifier);
                if(list != null)
                {
                    scope = StorageScope.Global;
                    return list;
                }
            }

            scope = StorageScope.Global;
            return null;
        }

        private List<object> GetList(IStorage storage, String identifier)
        {
            return storage.Get<List<object>>(identifier);
        }

        private void SetList(IContext context, StorageScope scope, String identifier, List<object> list)
        {
            IStorage storage = GetStorage(context, scope);
            storage.Set(identifier, list);
        }

        private void SetList(IStorage storage, String identifier, List<object> list)
        {
            storage.Set(identifier, list);
        }

        private IStorage GetStorage(IContext context, StorageScope scope)
        {
            switch(scope)
            {
                case StorageScope.Global:
                    return context.Bot.StorageManager.Global();
                case StorageScope.Server:
                    return context.Bot.StorageManager.Server(context.Message.Connection);
                case StorageScope.Channel:
                {
                    if(context.Message.Receiver.Type == MessageTargetType.Channel)
                    {
                        return context.Bot.StorageManager.Channel(context.Message.Receiver as IChannel);
                    }
                    return null;
                }
            }
            return null;
        }
    }
}
