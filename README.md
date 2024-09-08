# AdminAssist

A plugin for the Exiled SCP:SL framework that allows players to call for admin assistance using customizable commands.

## Description

AdminAssist is designed to enhance communication between players and server administrators in SCP: Secret Laboratory. This plugin enables players to request help from admins easily, with features like command aliases, cooldown management, and permission-based notifications.

## Features

- **Customizable Command Aliases**: Set multiple aliases for the command used to call for assistance.
- **Cooldown Management**: Prevent spam by configuring a cooldown period for command usage.
- **Permission-Based Notifications**: Specify which admin permissions will receive assistance requests.
- **Broadcast Messages**: Customize the messages sent to admins when a player calls for help.
- **Console Logging**: Log calls for assistance to the console for better oversight.

## Installation

1. Download the latest release of the AdminAssist plugin from the [releases page](https://github.com/intjiraya/AdminAssist/issues).
2. Place the downloaded `.dll` file into the `plugins` folder of your SCP:SL Exiled server.
3. Restart your server to load the plugin.
4. Configure the plugin settings in the `Config.yml` file.

## Configuration

The configuration file allows you to customize various settings for the plugin. Here’s an example of the configuration options:

```yaml
AdminAssist:
  # Plugin Enabled?
  is_enabled: true
  # Enable debug mode?
  debug: false
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
```

Adjust the values as needed to fit your server's requirements.

## Usage

Once installed and configured, players can call for admin assistance using the specified command. Admins with the appropriate permissions will receive notifications, and the configured broadcast message will be sent.

## Support

For any issues or questions regarding the AdminAssist plugin, please open an issue on the [GitHub repository](https://github.com/intjiraya/AdminAssist/issues) or join our community Discord server for assistance.

## Contributing

Contributions are welcome! If you would like to contribute to the development of AdminAssist, please fork the repository and submit a pull request. Ensure to follow coding standards and include appropriate documentation for your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/intjiraya/AdminAssist/blob/master/README.md) file for more details.