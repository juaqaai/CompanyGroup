﻿using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Logging;
using Orchard.Messaging.Events;
using Orchard.Messaging.Models;
using Orchard.ContentManagement.Records;

namespace Orchard.Messaging.Services {
    public class DefaultMessageManager : IMessageManager {
        private readonly IMessageEventHandler _messageEventHandler;
        private readonly IEnumerable<IMessagingChannel> _channels;

        public ILogger Logger { get; set; }

        public DefaultMessageManager(
            IMessageEventHandler messageEventHandler,
            IEnumerable<IMessagingChannel> channels) {
            _messageEventHandler = messageEventHandler;
            _channels = channels;
            Logger = NullLogger.Instance;
        }

        public void Send(ContentItemRecord recipient, string type, string service, Dictionary<string, string> properties = null) {
            if ( !HasChannels() )
                return;

            Logger.Information("Sending message {0}", type);
            try {

                var context = new MessageContext {
                    Recipient = recipient,
                    Type = type,
                    Service = service
                };

                try {

                    if (properties != null) {
                        foreach (var key in properties.Keys)
                            context.Properties.Add(key, properties[key]);
                    }

                    _messageEventHandler.Sending(context);

                    foreach (var channel in _channels) {
                        channel.SendMessage(context);
                    }

                    _messageEventHandler.Sent(context);
                }
                finally {
                    context.MailMessage.Dispose();
                }

                Logger.Information("Message {0} sent", type);
            }
            catch ( Exception e ) {
                Logger.Error(e, "An error occured while sending the message {0}", type);
            }
        }

        public bool HasChannels() {
            return _channels.Any();
        }

        public IEnumerable<string> GetAvailableChannelServices() {
            return _channels.SelectMany(c => c.GetAvailableServices());
        }
    }
}
