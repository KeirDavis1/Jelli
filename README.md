# Jelli

The do-everything bot

## Setup

Requirements:

- .NET Core 2.2

Run the following to set the bot up (you may need to restart your editor on Windows for the token to be applied to your environment)

```
setx token "<token>"
dotnet ef --project Jelli.ConsoleApp database update
```

**NOTES**:

- If running from Visual Studio, your running application directory is inside bin/Debug/ so the newly created DB file must be copied there.

## Commands

Commands are listed like the following:

```
<argument> - A single word argument you can pass through
<argument*> - A multi-word argument you can pass through
```

| Type           | Name        | Command                      | Description                                                                                                                                                             | Requirements                                                               |
| -------------- | ----------- | ---------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------- |
| Administration | Ban         | `!ban <user> <reason*>`      | The bot will ban the `<user>` and associate the `<reason*>` for the ban with it.                                                                                        | Bot requires **ban** permission. Issuing user requires **ban** permission. |
| Administration | Set Prefix  | `!setprefix <prefix>`        | The bot will set the prefix for the server to `<prefix>`. If you can no longer access it, you can mention the bot and run the command without the command prefix (`!`). |                                                                            |
| Administration | Create Note | `!notes + <user> <message*>` | Adds a note about a user in the guild                                                                                                                                   |                                                                            |
| Administration | List Notes  | `!notes list <user>`         | List the notes about a user in the guild                                                                                                                                |                                                                            |
| Basic          | Echo        | `!echo <message*>`           | The bot will respond with `<message*>` in the channel the user issued the command.                                                                                      |                                                                            |
| Basic          | User Info   | `!userinfo <user>`           | The bot will send a formatted message displaying when the user joined discord, user joined the current server and their user ID.                                        | The command must be run in a server.                                       |
| Basic          | Version     | `!version`                   | The bot will respond with the applications version in the channel the user issued the command.                                                                          |                                                                            |
| Basic          | Ping        | `!ping`                      | The bot will respond with "Pong!" in the channel the user issued the command.                                                                                           |                                                                            |

## Contributing

_TODO, Contribute to the contributing section_

### Database Changes

Did you make a database change? Don't forget to:

1. Make your changes

2. Create a new migration

`dotnet ef --project Jelli.Data migrations add <MigrationName>`

3. Update the database

`dotnet ef --project Jelli.ConsoleApp database update`
