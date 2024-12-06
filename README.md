# ReenbitChat

**ReenbitChat** is a real-time chat application built using ASP.NET Core, SignalR, Redis, and Blazor for interactive real-time communication. The project allows users to create chat rooms and interact through messages, providing a user-friendly interface and scalable architecture.

## Features
- Uses **SignalR** for real-time message delivery.
- Supports **Redis** for caching and session storage.
- The interface is built with **Blazor** for fast and interactive user experiences.
- Basic user and chat room management features are implemented.

## Setup Instructions

1. Clone the repository:

    ```bash
    git clone https://github.com/vityok02/ReenbitChat.git
    ```

2. Navigate to the project directory:

    ```bash
    cd ReenbitChat
    ```

3. Install the required packages:

    Use the .NET CLI to restore dependencies:

    ```bash
    dotnet restore
    ```

4. Configure the Redis connection:

    Open the `appsettings.json` file and add the correct Redis connection string:

    ```json
    {
      "ConnectionStrings": {
        "Redis": "localhost"
      }
    }
    ```

5. Run the server:

    To start the API and client, use the following command:

    ```bash
    dotnet run --project ReenbitChat
    ```

6. Run the Blazor client:

    ```bash
    dotnet run --project ReenbitChatClient
    ```

7. Open your web browser and go to:

    ```
    https://localhost:3000
    ```

## How to Use

1. Run the application.
2. Open the web page in your browser.
3. Create a chat room or join an existing one.
4. Send messages, and they will be instantly delivered to other users via SignalR.
5. Use sentiment analysis for each message if it's enabled.

