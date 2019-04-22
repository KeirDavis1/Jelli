# Jelli

The do-everything bot

## Setup

Requirements:

- .NET Core 2.2

Run the following to set the Bot Token up (you may need to restart your editor on Windows)

```
setx token "<token>"
dotnet ef --project Jelli.ConsoleApp database update
```

NOTES:
- If running from Visual Studio, your running application directory is inside bin/Debug/ so the newly created DB file must be copied there.

## Commands

Commands are listed like the following:

```
<argument> - A single word argument you can pass through
<argument*> - A multi-word argument you can pass through
```

**Administration**

- Ban

  `!ban <user> <reason*>`

  _The bot will ban the `<user>` and associate the `<reason*>` for the ban with it._

  - Bot requires **ban** permission.
  - Issuing user requires **ban** permission.

- Set Prefix

  `!setprefix <prefix>`

  _The bot will set the prefix for the server to `<prefix>`. If you can no longer access it, you can
  mention the bot and run the command without the command prefix (`!`).

**Basic**

- Echo

  `!echo <message*>`

  _The bot will respond with `<message*>` in the channel the user issued the command._

- User Info

  `!userinfo <?user>`

  _The bot will send a formatted message displaying when the user joined discord, user joined the current server and their user ID._

  - The command must be run in a server.

- Version

  `!version`

  _The bot will respond with the applications version in the channel the user issued the command._

- Ping

  `!ping`

  _The bot will respond with "pong" in the channel the user issued the command._

## Contributing

_TODO, Contribute to the contributing section_
