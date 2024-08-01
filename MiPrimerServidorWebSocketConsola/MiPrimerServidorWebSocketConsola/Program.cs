// See https://aka.ms/new-console-template for more information
using Fleck;
string ipServidor = "ws://192.168.209.72:9001";
WebSocketServer server = new WebSocketServer(ipServidor);
List<IWebSocketConnection> clienteConexiones = new List<IWebSocketConnection>();
Console.WriteLine("Inicio el servidor Web Socket");
server.Start(cliente =>
{
    cliente.OnOpen = () =>
    {
        clienteConexiones.Add(cliente);
        Console.WriteLine("Se conecto un cliente con Ip " + cliente.ConnectionInfo.ClientIpAddress);
    };
    cliente.OnMessage = (string mensaje) =>
    {
        clienteConexiones.ForEach(p => p.Send(mensaje));
        Console.WriteLine(" Se notifico al socket el mensaje " + mensaje);
    };
    cliente.OnClose = () =>
    {
        clienteConexiones.Remove(cliente);
        Console.WriteLine("Se desconecto el cliente con Ip " + cliente.ConnectionInfo.ClientIpAddress);
    };
});
Console.ReadLine();