using Akka.Actor;

namespace ActorSystemAsAContinuation
{
    public class ContinuationActor:ReceiveActor
    {
        public ContinuationActor()
        {
            Receive<string>(ProcessMsge);
        }

        private void ProcessMsge(string msg)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 60 == 0)
                    Console.Write("'");
                Thread.Sleep(1000);
            }

            Console.WriteLine();

            Console.WriteLine("Finished        actor" + msg);
        }
    }
}
