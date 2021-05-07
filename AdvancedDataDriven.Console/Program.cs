﻿using System;
using AdvancedDataDriven.Rx;

namespace AdvancedDataDriven.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run the Reactive Redis Listener
            RunRedisReactiveListener();
        }

        static void OnStreamDataAvailable(StreamEntriesArgs sea)
        {
            int i = 0;
            while (i <= sea.StreamEntries.Length - 1)
            {
                Console.WriteLine(sea.StreamEntries[i].Id);
                i++;
            }
        }
        static void RunRedisReactiveListener()
        {
            var r = new RedisStreamRxManager();

            var listener = r.RedisStreamRxSubscriber("myStream"); //Ensure this stream exists

            using (IDisposable disposable = listener.Subscribe(new RedisObserver(OnStreamDataAvailable)))
            {
                Console.ReadLine();
            }
        }
    }
}
