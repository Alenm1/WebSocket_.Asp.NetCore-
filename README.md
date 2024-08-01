# Sistema de Gestión de Cursos con ASP.NET WebSockets

Este proyecto demuestra un sistema de gestión de cursos utilizando WebSockets en ASP.NET. Incluye el servidor y cliente que pueden ejecutarse en diferentes dispositivos.

### Instrucciones

1. **Descargar el repositorio** en tus dispositivos.

2. **Iniciar la aplicación del servidor**. Abre una terminal y navega hasta el directorio `ServerApp`, luego ejecuta el siguiente comando:
    ```bash
    dotnet run
    ```

3. **Iniciar la aplicación del cliente** en otros dispositivos. Abre una terminal en cada dispositivo cliente, navega hasta el directorio `ClientApp` y ejecuta el siguiente comando:
    ```bash
    dotnet run
    ```

### Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado en los dispositivos cliente y servidor.

### Nota

Asegúrate de cambiar la dirección IP en "MiProyectoNetCoreSocket" en `appsettings.json` para que apunten a la IP del servidor en:
"urlsocket": "ws://192.168.209.72:9001"

### Enlace al Proyecto

Visita nuestro proyecto en el siguiente enlace:
- 🌐 [Sistema de Gestión de Cursos](http://www.cursosuna.somee.com/Carrito/Index)

---

¡Gracias por usar nuestro Sistema de Gestión de Cursos!
