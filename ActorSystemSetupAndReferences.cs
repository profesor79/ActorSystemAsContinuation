using Akka.Actor;
using Akka.Routing;

namespace ActorSystemAsAContinuation
{
    public static class ActorSystemSetupAndReferences
    {
       public static ActorSystem ActorSystemRef;
        public static IActorRef ContiueActorRef;
         static ActorSystemSetupAndReferences()
        {
            ActorSystemRef = Akka.Actor.ActorSystem.Create("actorSystem");
            ContiueActorRef = ActorSystemRef.ActorOf(Props.Create(typeof(ContinuationActor)).WithRouter(new RoundRobinPool(50)));
        }
    }
}
