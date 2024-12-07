# AdminAssist

A plugin for the SCP: Secret Laboratory server with the [EXILED framework](https://github.com/ExMod-Team/EXILED) installed, allowing easy communication with server administration through a single command.

## Description

The plugin is designed to enhance interaction between players and server administration. AdminAssist enables players to easily request help or report issues to staff members with special access.

## Features

- **Custom Command Aliases**: Set multiple aliases for the command used to call for help.
- **Cooldown Management**: Prevent spam by configuring a cooldown for command usage.
- **Permission-Based Notifications**: Specify which administrators will receive help requests based on their permissions.
- **Message Customization**: Fully customize the messages that administrators will receive.

## Installation

1. Download the latest version of the plugin from the [releases page](https://github.com/intjiraya/AdminAssist/releases).
2. Move the installed file `AdminAssist.dll` to the `Plugins` folder of your SCP:SL server.
3. Completely restart the server.

## Configuration

The configuration file allows you to set various parameters for the plugin. Here is an example of the configuration parameters:

```yaml
AdminAssist:
  # Command aliases for calling admins
  command_aliases: 
    - call
    - admin
  # Cooldown time in seconds
  cooldown: 0
  # Permissions for receiving requests
  permissions:
    - CallNotify
    - PlayerSensitiveDataAccess
  # Broadcast message to admins
  admins_broadcast: "<color=#FFA500>(%id%) %nickname% called the admins</color>"
  # Duration of the broadcast message
  admins_broadcast_duration: 5
  # If true, all players with RA permissions will receive requests.
  to_all_ra_authorized: true
```

Edit the default values as needed according to your server's requirements.

## Usage

After installation and configuration, players can request help from an administrator using the specified command. Administrators with the appropriate permissions will receive notifications, and a customized broadcast message will be sent to them.

## Support

For any questions or issues related to the plugin, please open an issue in the [GitHub repository](https://github.com/intjiraya/AdminAssist/issues).