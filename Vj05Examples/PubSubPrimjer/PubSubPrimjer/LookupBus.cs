using System;
using Akka.Actor;
using Akka.Event;

namespace PubSubPrimjer
{
    public class MsgEnvelope
    {
        public string Topic { get; private set; }
        public Object Payload { get; private set; }

        public MsgEnvelope(string topic, object payload)
        {
            Topic = topic;
            Payload = payload;
        }
    }

    // 1. tip - tip eventa koji se publisha
    // 2. tip - tip klasifikatora za klasifikaciju evenata
    // 3. tip - tip subscribera koji mogu slušati na event
    public class LookupBus : EventBus<MsgEnvelope, String, IActorRef> 
    {
        // je li jedna klasa podklasa sdruge
        protected override bool IsSubClassification(string parent, string child)
        {
            return true;
        }

        // publisha događaj (event) subscriberu
        protected override void Publish(MsgEnvelope @event, IActorRef subscriber)
        {
            subscriber.Tell(@event);
        }

        // 1. argument - događaj (event) koji se klasificira
        // 2. argument - klasifikator koji se koristi da se klasificira događaj
        protected override bool Classify(MsgEnvelope @event, string classifier)
        {
            return @event.Topic == classifier;
        }
        
        // dobavlja vrijednost kojom klasificiramo pojedini događaj
        protected override string GetClassifier(MsgEnvelope @event)
        {
            return @event.Topic;
        }
    }
}