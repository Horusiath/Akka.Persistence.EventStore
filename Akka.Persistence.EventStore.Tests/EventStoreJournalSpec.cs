using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.TestKit;
using Akka.Persistence;
using Akka.Persistence.Journal;
using Xunit;
using Akka.Persistence.TestKit.Journal;
using Akka.Configuration;
using Xunit.Abstractions;


namespace Akka.Persistence.EventStore.Tests
{
    public class EventStoreJournalSpec : JournalSpec
    {
        private static readonly Config SpecConfig = ConfigurationFactory.ParseString(@"
            akka {
                stdout-loglevel = DEBUG
	            loglevel = DEBUG

                persistence {

                publish-plugin-commands = off
                journal {
                    plugin = ""akka.persistence.journal.event-store""
                    event-store {
                        class = ""EventStore.Persistence.EventStoreJournal, Akka.Persistence.EventStore""
                        plugin-dispatcher = ""akka.actor.default-dispatcher""
                        
                        # the event store connection string
			            connection-string = ""ConnectTo=tcp://admin:changeit@127.0.0.1:1113;""

			            # name of the connection
			            connection-name = ""akka.net""
                    }
                }
            }
        }
        ");
        public EventStoreJournalSpec(ITestOutputHelper output)
            : base(SpecConfig, "EventStoreJournalSpec", output: output) 
        {
            Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //cleanup
            StorageCleanup.Clean();
        }
    }
}
