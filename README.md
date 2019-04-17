# Jelli

The do-everything bot

## Setup

Requirements:

- .NET Core 2.2

Run the following to set the Bot Token up (you may need to restart your editor on Windows)

```
setx token "<token>"
```

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

**Basic**

- Echo

  `!echo <message*>`

  _The bot will respond with `<message*>` in the channel the user issued the command._

- User Info

  `!userinfo <?user>`

  _The bot will send a formatted message displaying when the user joined discord, user joined the current server and their user ID._

  - The command must be run in a server.

## Contributing

_TODO, Contribute to the contributing section_

---

## Todo

- Verification

  Allows a server to implement a lobby-based or verification system to allow active members to be verified into a community.

- Role Management

  Allows a server to implement roles based on time in server, a user clicking on a reaction or using a command.
