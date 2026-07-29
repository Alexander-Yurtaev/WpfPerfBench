# WpfPerfBench

***Внимание!*** Проект находится в разработке. Еще не весь функционал реализован. README появится позже. 
На текущий момент можно протестировать первые шаги.
Для этого
- Разверните в Docker Postgres
docker compose -f docker-compose-postgres.yml up -d
- Либо создайте пустую БД
- Запустите приложение
На первом экране главное значение имее ConnectionString, которое используется для подключения к БД.
Если Вы создали БД командой выше, то используйте
- Host=localhost;Port=5432;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26
Другие поля служат для демонстрации валидации.
- Других дополнительных настроек приложение не требует.

Реализована поддержка ***только*** Postgres.

```bash
# Для Postgres
docker compose -f docker-compose-postgres.yml up -d

# Для MS SQL
docker compose -f docker-compose-mssql.yml up -d
```

```bash
cd src\WpfPerfBench.Data
```

```bash
# Для Postgres
dotnet ef migrations add InitialPostgres --context PostgresDataContext --output-dir Migrations/Postgres -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Для MS SQL
dotnet ef migrations add InitialMsSql --context MsSqlDdataContext --output-dir Migrations/MsSql -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
```

```bash
# Для Postgres
dotnet ef database update --context PostgresDataContext -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Для MS SQL
dotnet ef database update --context MsSqlDdataContext -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
```

```bash
# Для Postgres
dotnet ef migrations remove --context PostgresDataContext -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Для MS SQL
dotnet ef migrations remove --context SqliteDdataContext -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
```
