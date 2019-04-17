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

  The bot will ban the `<user>` and associate the `<reason*>` for the ban with it.

  - Bot requires **ban** permission.
  - Issuing user requires **ban** permission.

**Basic**

- Echo

  `!echo <message*>`

  The bot will respond with `<message*>` in the channel the user issued the command.

