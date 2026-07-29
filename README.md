# WpfPerfBench

**Внимание! Проект находится в разработке. Еще не весь функционал реализован. README появится позже. На текущий момент можно **

**Реализована поддержка только Postgres**

```bash
# Äëÿ Postgres
docker compose -f docker-compose-postgres.yml up -d

# Äëÿ MS SQL
docker compose -f docker-compose-mssql.yml up -d
```

```bash
cd src\WpfPerfBench.Data
```

```bash
# Äëÿ Postgres
dotnet ef migrations add InitialPostgres --context PostgresDataContext --output-dir Migrations/Postgres -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Äëÿ MS SQL
dotnet ef migrations add InitialMsSql --context MsSqlDdataContext --output-dir Migrations/MsSql -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
```

```bash
# Äëÿ Postgres
dotnet ef database update --context PostgresDataContext -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Äëÿ MS SQL
dotnet ef database update --context MsSqlDdataContext -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
```

```bash
# Äëÿ Postgres
dotnet ef migrations remove --context PostgresDataContext -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Äëÿ MS SQL
dotnet ef migrations remove --context SqliteDdataContext -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
```
