# Установка Обучающей игры по SQL
1. Установите Unity версии 2022.3.9f1.
2. Скачайте и откройте проект через Unity Hub.
3. Найдите файл DatabaseConnector.cs и измените строку подключения к базе данных:
```
private static string connectionString = "Server=localhost;Port=5432;Database=gameSQL;User Id=postgres;Password=admin;";
```
4. Создайте базу данных, соответствующую структуре, приведенной ниже:
![](/DBModel.PNG)
5. Заполните таблицы данными, создайте хотя бы одного пользователя.
6. В Unity откройте сцену StartPage и запустите проект.
