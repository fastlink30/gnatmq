using System;
using uPLibrary.Networking.M2Mqtt;

#if TRACE
// alias needed due to Microsoft.SPOT.Trace in .Net Micro Framework
// (it's ambiguos with uPLibrary.Networking.M2Mqtt.Utility.Trace)
using MqttUtility = uPLibrary.Networking.M2Mqtt.Utility;
#endif

namespace GnatMQServer
{
    class Program
    {
        static void Main(string[] args)
        {
#if TRACE
            MqttUtility.Trace.TraceLevel = MqttUtility.TraceLevel.Verbose | MqttUtility.TraceLevel.Frame;
            MqttUtility.Trace.TraceListener = (f, a) => System.Diagnostics.Trace.WriteLine(String.Format(f, a));
#endif

            // create and start broker
            MqttBroker broker = new MqttBroker();

            broker.BrokerStart += Broker_BrokerStart;
            broker.BrokerStop += Broker_BrokerStop;
            broker.ClientClose += Broker_ClientClose;
            broker.ClientConnected += Broker_ClientConnected;
            broker.ClientConnectionClosed += Broker_ClientConnectionClosed;
            broker.ClientMsgDisconnected += Broker_ClientMsgDisconnected;
            broker.ClientMsgPublishReceived += Broker_ClientMsgPublishReceived;
            broker.ClientMsgSubscribeReceived += Broker_ClientMsgSubscribeReceived;
            broker.ClientMsgUnsubscribeReceived += Broker_ClientMsgUnsubscribeReceived;

            broker.Start();

            Console.ReadLine();

            broker.Stop();
        }

        private static void Broker_ClientMsgUnsubscribeReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgUnsubscribeEventArgs e)
        {
            Console.WriteLine($"Client UnsubscribeReceived");
        }

        private static void Broker_ClientMsgSubscribeReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgSubscribeEventArgs e)
        {
            Console.WriteLine($"Client SubscribeReceived");
        }

        private static void Broker_ClientMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            Console.WriteLine($"Client PublishReceived");
        }

        private static void Broker_ClientMsgDisconnected(object sender, string ClientId)
        {
            Console.WriteLine($"Client [{ClientId}] Disconnected");
        }

        private static void Broker_ClientConnectionClosed(object sender, string ClientId)
        {
            Console.WriteLine($"Client [{ClientId}] Connection closed");
        }

        private static void Broker_ClientConnected(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgConnectEventArgs e)
        {
            Console.WriteLine($"Client [{e.Message.ClientId}] Connected");
        }

        private static void Broker_ClientClose(object sender, string ClientId)
        {
            Console.WriteLine($"Client [{ClientId}] Close");
        }

        private static void Broker_BrokerStop(object sender, EventArgs e)
        {
            Console.WriteLine("Broker Stop");
        }

        private static void Broker_BrokerStart(object sender, EventArgs e)
        {
            Console.WriteLine("Broker Start");
        }
    }
}
