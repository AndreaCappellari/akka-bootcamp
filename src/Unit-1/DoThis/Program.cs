﻿using System;
using Akka.Actor;

namespace WinTail;

#region Program
class Program
{
    public static ActorSystem MyActorSystem;

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        // initialize MyActorSystem
        MyActorSystem =ActorSystem.Create("MyActorSystem");

        Props consoleWriterProps = Props.Create(typeof (ConsoleWriterActor));
        
        IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps,
            "consoleWriterActor");
        
        Props validationActorProps = Props.Create(
            () => new ValidationActor(consoleWriterActor));
        
        IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, 
            "validationActor");
        
        Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
        
        IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps,
            "consoleReaderActor");
        
       consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);
        
        // blocks the main thread from exiting until the actor system is shut down
        MyActorSystem.WhenTerminated.Wait();
    }

   
}
#endregion