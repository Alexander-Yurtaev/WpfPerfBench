```bash
# Для Postgres
docker compose -f docker-compose-postgres.yml up -d

# Для MS SQL
docker compose -f docker-compose-mssql.yml up -d
```

```bash
cd src\WpfPrefBench.Data
```

```bash
# Для Postgres
dotnet ef migrations add InitialPostgres --context PostgresDataContext --output-dir Migrations/Postgres -- "Host=localhost;Database=wpf_pref_bench;Username=postgres;Password=Wpf_Pref_Bench_26;"

# Для MS SQL
dotnet ef migrations add InitialSqlite --context MsSqlDdataContext --output-dir Migrations/Sqlite -- Server=localhost;Database=wpf_pref_bench;User Id=sa;Password=Wpf_Pref_Bench_26;TrustServerCertificate=true;
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