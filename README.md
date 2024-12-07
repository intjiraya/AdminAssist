# AdminAssist

Плагин для сервера в SCP: Secret Laboratory с установленным [EXILED фреймворком](https://github.com/ExMod-Team/EXILED), позволяющий легко обращаться к администрации сервера посредство одной команды.

## Описание

Плагин предназначен для улучшения взаимодействия между игроками и администрацией сервера. AdminAssist позволяет игрокам легко запрашивать помощь или сообщать о чем-либо у сотрудников, у которых есть специальный доступ.

## Функции

- **Настраиваемые псевдонимы команд**: Установите несколько псевдонимов для команды, используемой для вызова помощи.
- **Управление задержкой**: Предотвратите спам, настроив задержку для использования команд.
- **Уведомления на основе разрешений**: Укажите, администраторы с какими правами будут отправляться запросы о помощи.
- **Настройка сообщений**: Свободная настройка сообщений, которые будут получать администраторы.

## Installation

1. Скачайте последнюю версию плагина со [страницы релизов](https://github.com/intjiraya/AdminAssist/releases).
2. Переместите установленный файл `AdminAssist.dll` в папку `Plugins` вашего SCP:SL сервер.
3. Полностью перезапустите сервер.

## Configuration

Файл конфигурации позволяет вам настроить различные параметры для плагина. Вот пример параметров конфигурации:

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

Отредактируйте изначальные значения по мере необходимости в соответствии с требованиями вашего сервера.

## Использование

После установки и настройки игроки могут обратиться за помощью к администратору, используя указанную команду. Администраторы с соответствующими разрешениями будут получать уведомления, и им будет отправлено настроенное широковещательное сообщение.

## Support

Для любых вопросов или проблем, связанных с плагином, пожалуйста, откройте проблему в [репозитории GitHub](https://github.com/intjiraya/AdminAssist/issues).