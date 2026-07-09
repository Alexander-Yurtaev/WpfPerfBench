@echo off
REM Script for managing Docker Compose with MSSQL and Postgres files

setlocal enabledelayedexpansion

REM --- Configuration ---
REM basic Specify the project name. It will be added to the container names.
REM It is very important that the databases of the data were not considered each other for silence.
set "PROJECT_NAME=myapp"

REM Try using docker-create a file (automatically scripts)
set "MSSQL_FILE=docker-compose-mssql.yml"
set "POSTGRES_FILE=docker-composer-postgres.yml"
REM --------------------

if "%~1"=="" (
    echo Use: %~nx0 ^<mssql^|postgres^> ^<up^|down^>
    exit /b 1
)

set "DB_TYPE=%~1"
set "ACTION=%~2"

call :to_lower DB_TYPE
call :to_lower ACTION

if not "!DB_TYPE!"=="mssql" if not "!DB_TYPE!"=="postgres" (
    echo Error: Unknown database '!DB_TYPE!'. Use 'mssql' or 'postgres'.
    exit /b 1
)

if not "!ACTION!"=="up" if not "!ACTION!"=="down" (
    echo Error: Unknown action '!ACTION!'. Use 'up' or 'down'.
    exit /b 1
)

REM Selecting a configuration file based on an argument
if "!DB_TYPE!"=="mssql" set "COMPOSE_FILE=!MSSQL_FILE!"
if "!DB_TYPE!"=="postgres" set "COMPOSE_FILE=!POSTGRES_FILE!"

REM Checking the existence of the selected file
if not exist "!COMPOSE_FILE!" (
    echo Error: File !COMPOSE_FILE! not found.
    exit /b 1
)

echo Performing an action [!ACTION!] for [!DB_TYPE!]...

REM The basic logic of starting/stopping
if "!ACTION!"=="up" (
    docker compose -f "!COMPOSE_FILE!" --project-name "!PROJECT_NAME!" up -d
) else (
    docker compose -f "!COMPOSE_FILE!" --project-name "!PROJECT_NAME!" down
)

exit /b 0

:: Auxiliary function for converting a line to lowercase
:to_lower
for %%L in (A B C D E F G H I J K L M N O P Q R S T U V W X Y Z) do (
    set "%1=!%1:%%L=%%L!"
)
goto :eof