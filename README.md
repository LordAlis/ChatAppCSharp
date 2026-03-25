# Chat Application - C# OOP

A TCP socket-based Server-Client chat application built with Windows Forms and .NET 8.

## Project Structure

- **ChatApp.Common** - Shared models, interfaces, and helper classes (Class Library)
- **ChatApp.Server** - Server application (Windows Forms)
- **ChatApp.Client** - Client application (Windows Forms)

## OOP Concepts

| Concept | Usage |
|---------|-------|
| Interface | `INetworkService`, `IMessageHandler` |
| Abstract Class | `NetworkServiceBase` |
| Inheritance | `ChatServer : NetworkServiceBase`, `ChatClient : NetworkServiceBase` |
| Encapsulation | Private fields, public properties |
| Events/Delegates | `MessageReceived`, `ClientConnected`, `ConnectionLost` |
| Enum | `MessageType` (Text, Join, Leave, ServerMessage) |
| Polymorphism | Start/Stop via `INetworkService` |

## Prerequisites

- .NET 8 SDK
- Windows OS

## Build

```
dotnet build ChatApp.sln
```

## Usage

1. **Start the server:** Run `ChatApp.Server`, enter a port (e.g. 5000), click "Start"
2. **Start the client:** Run `ChatApp.Client` (multiple instances allowed)
3. **Connect:** Enter IP `127.0.0.1`, port `5000`, pick a username, click "Connect"
4. **Chat:** Type a message and press Enter or click "Send"

## Running with Visual Studio

1. Open `ChatApp.sln` in Visual Studio
2. Right-click Solution > "Set Startup Projects" > "Multiple startup projects"
3. Set both Server and Client to "Start"
4. Press F5 to run
