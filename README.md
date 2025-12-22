# Интеграция с ФИАС
Репозиторий с шаблоном разработки "Интеграция с ФИАС ([Открытые API-сервисы ФИАС](https://fias.nalog.ru/Frontend))".
Подробное описание см. в docs\Описание шаблона разработки Интеграция с ФИАС.docx.
> [!NOTE]
> Замечания и пожеланию по развитию шаблона разработки фиксируйте через [Issues](https://github.com/DirectumCompany/rx-template-fias/issues).
При оформлении ошибки, опишите сценарий для воспроизведения. Для пожеланий приведите обоснование для описываемых изменений - частоту использования, бизнес-ценность, риски и/или эффект от реализации.
> 
> Внимание! Изменения будут вноситься только в новые версии.

## Порядок установки
Для работы требуется:
+ Directum RX версии 4.10 и выше.
+ Открытый доступ к API-сервисам со всех веб-серверов системы.
+ Токен для предоставления доступа к API-сервисам (порядок получения описан в [Условиях использования Открытых API-сервисов ФИАС](https://fias.nalog.ru/docs/%D0%A3%D1%81%D0%BB%D0%BE%D0%B2%D0%B8%D1%8F%20%D0%B8%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F%20API-%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%BE%D0%B2%20%D0%A4%D0%98%D0%90%D0%A1.pdf), [Форма заявки на подключение](https://fias.nalog.ru/docs/%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%20%D0%B7%D0%B0%D1%8F%D0%B2%D0%BA%D0%B8.docx)).

### Установка для ознакомления
1. Склонировать репозиторий с rx-template-fias в папку.
2. Указать в config.yml в разделе DevelopmentStudio:
```xml
   GIT_ROOT_DIRECTORY: '<Папка из п.1>'
   REPOSITORIES:
      repository:
      -   '@folderName': 'work'
          '@solutionType': 'Work'
          '@url': https://github.com/DirectumCompany/rx-template-fias.git'
      -   '@folderName': 'base'
          '@solutionType': 'Base'
          '@url': ''
```

### Установка для использования на проекте
Возможные варианты:

#### A. Fork репозитория
1. Сделать fork репозитория rx-template-fias для своей учетной записи.
2. Склонировать созданный в п. 1 репозиторий в папку.
3. Указать в config.yml в разделе DevelopmentStudio:
```xml
   GIT_ROOT_DIRECTORY: '<Папка из п.2>'
   REPOSITORIES:
      repository:
      -   '@folderName': 'work'
          '@solutionType': 'Work'
          '@url': https://github.com/DirectumCompany/rx-template-fias.git'
      -   '@folderName': 'base'
          '@solutionType': 'Base'
          '@url': ''
```

#### B. Подключение на базовый слой.
Вариант не рекомендуется, так как при выходе версии шаблона разработки не гарантируется обратная совместимость.
1. Склонировать репозиторий rx-template-fias в папку.
2. Указать в config.yml в разделе DevelopmentStudio:
```xml
   GIT_ROOT_DIRECTORY: '<Папка из п.1>'
   REPOSITORIES:
      repository:
      -   '@folderName': 'work'
          '@solutionType': 'Work'
          '@url': '<Адрес репозитория для рабочего слоя>'
      -   '@folderName': 'base'
          '@solutionType': 'Base'
          '@url': ''
      -   '@folderName': 'base'
          '@solutionType': 'Base'
          '@url': 'https://github.com/DirectumCompany/rx-template-fias.git'
```

#### C. Копирование репозитория в систему контроля версий.
Рекомендуемый вариант для проектов внедрения.
1. В системе контроля версий с поддержкой git создать новый репозиторий.
2. Склонировать репозиторий rx-template-fias в папку с ключом `--mirror`.
3. Перейти в папку из п. 2.
4. Импортировать клонированный репозиторий в систему контроля версий командой:
`git push –mirror <Адрес репозитория из п. 1>`
