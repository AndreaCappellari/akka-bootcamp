﻿using System;
using Akka.Actor;

namespace WinTail;

public class ValidationActor : UntypedActor
{
    private readonly IActorRef _consoleWriterActor;

    public ValidationActor(IActorRef consoleWriterActor)
    {
        _consoleWriterActor = consoleWriterActor;
    }

    protected override void OnReceive(object message)
    {
        var msg = message as string;
        if (string.IsNullOrEmpty(msg))
        {
            // signal that the user needs to supply an input
            _consoleWriterActor.Tell(
                new Messages.NullInputError("No input received."));
        }
        else
        {
            if (IsValid(msg))
            {
                // send success to console writer
                _consoleWriterActor.Tell(new Messages.InputSuccess("Thank you! Message was valid."));
            }
            else
            {
                // signal that input was bad
                _consoleWriterActor.Tell(new Messages.ValidationError("Invalid: input had odd number of characters."));
            }
        }

        Sender.Tell(new Messages.ContinueProcessing());
    }

    private static bool IsValid(string message)
    {
        var valid = message.Length % 2 == 0;
        return valid;
    }
}