using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ICommandReceiver
{
    private Dictionary<ECommand, Delegate> mCommands = new Dictionary<ECommand, Delegate>();

    public void AddCommand<T>(ECommand command, CommandHandler<T> handler) where T : ICommand
    {
        if (!this.mCommands.ContainsKey(command))
        {
             this.mCommands.Add(command, handler);
        }
    }

    public ECommandReply Command<T>(T cmd) where T : ICommand
    {
        Delegate del = null;
        mCommands.TryGetValue(cmd.Command, out del);
        if (del == null)
        {
            return ECommandReply.N;
        }
        CommandHandler<T> callback = del as CommandHandler<T>;
        if (callback == null)
        {
            Debug.LogError(typeof(T).ToString() + cmd);
            return ECommandReply.N;
        }
        return callback.Invoke(cmd);
    }
}
