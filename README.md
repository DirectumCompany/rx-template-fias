# Примеры перекрытий прикладной разработки
Репозиторий с шаблоном разработки "Интеграция с ФИАС ([Открытые API-сервисы ФИАС](https://fias.nalog.ru/Frontend))".
Подробное описание см. в docs\Описание шаблона разработки Интеграция с ФИАС.docx.

## Порядок установки
Для работы требуется:
+ Directum RX версии 4.10 и выше.
+ Открытый доступ к API-сервисам со всех веб-серверов системы.
+ Токен для предоставления доступа к API-сервисам (порядок получения описан в [Условиях использования Открытых API-сервисов ФИАС](https://fias.nalog.ru/docs/%D0%A3%D1%81%D0%BB%D0%BE%D0%B2%D0%B8%D1%8F%20%D0%B8%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F%20API-%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%BE%D0%B2%20%D0%A4%D0%98%D0%90%D0%A1.pdf) [Форма заявки на подключение](https://fias.nalog.ru/docs/%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%20%D0%B7%D0%B0%D1%8F%D0%B2%D0%BA%D0%B8.docx).

### Установка для ознакомления
Склонировать репозиторий rx-template-fias в папку.
Указать в _ConfigSettings.xml DDS:
```
<block name="REPOSITORIES">
  <repository folderName="Base" solutionType="Base" url="" />
  <repository folderName="RX" solutionType="Base" url="<адрес локального репозитория>" />
  <repository folderName="<Папка из п.1>" solutionType="Work" 
     url="https://github.com/DirectumCompany/rx-template-fias" />
</block>
```
### Установка для использования на проекте
Возможные варианты:

#### A. Fork репозитория

Сделать fork репозитория rx-template-fias для своей учетной записи.
Склонировать созданный в п. 1 репозиторий в папку.
Указать в _ConfigSettings.xml DDS:
```
<block name="REPOSITORIES">
  <repository folderName="Base" solutionType="Base" url="" /> 
  <repository folderName="<Папка из п.2>" solutionType="Work" 
     url="<Адрес репозитория gitHub учетной записи пользователя из п. 1>" />
</block>
```
#### B. Подключение на базовый слой.

Вариант не рекомендуется, так как при выходе версии шаблона разработки не гарантируется обратная совместимость.

Склонировать репозиторий rx-template-fias в папку.
Указать в _ConfigSettings.xml DDS:
```
<block name="REPOSITORIES">
  <repository folderName="Base" solutionType="Base" url="" /> 
  <repository folderName="<Папка из п.1>" solutionType="Base" 
     url="<Адрес репозитория gitHub>" />
  <repository folderName="<Папка для рабочего слоя>" solutionType="Work" 
     url="https://github.com/DirectumCompany/rx-template-fias" />
</block>
```
#### C. Копирование репозитория в систему контроля версий.

Рекомендуемый вариант для проектов внедрения.

В системе контроля версий с поддержкой git создать новый репозиторий.
Склонировать репозиторий rx-template-fias в папку с ключом 
```
--mirror.
```
Перейти в папку из п. 2.
Импортировать клонированный репозиторий в систему контроля версий командой:
```
git push –mirror <Адрес репозитория из п. 1>
```
