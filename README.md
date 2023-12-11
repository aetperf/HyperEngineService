# Hyper Engine

Download the latest release here :
https://tableau.github.io/hyper-db/docs/releases#download
unzip it and locate the hyperd.exe file
You can use this file to start an hyper engine

The HyperEngineService is a wrapper around hyperd.exe to start it as a windows service


# HyperEngineService

Copy your hyperd.exe file in a folder : for exemple D:\HyperEngine\hyperd.exe

Compile this project, publish it, or use the released file associated in the repo and put the files in the same folder as hyperd.exe

Open a powershell prompt as administrator and run this command :

```
$HYPERENGINE_HOME="D:\HyperEngine"
sc create HyperEngineService binPath= "${HYPERENGINE_HOME}\HyperEngineService.exe" DisplayName="Hyper Database Engine" start=demand
```

The service is created but not started. You can start it with the services.msc console or using the command :

Then you can start the service with this command :

```
sc start HyperEngineService
```

Then you can stop the service with this command :

```
sc stop HyperEngineService
```

You can delete the service with this command :

```	
sc delete HyperEngineService
```

# HyperEngineClient

Use a **postgreSQL client** (JDBC, ODBC, psql, ngpgsql,...) to connect to hyper engine and attach a database :
host : where you start the service (localhost if you start it on your computer)
port : 8095
user : tableau_internal_user
password : 
database : the path to your hyper file accessible from the host

SQL Dialect : https://tableau.github.io/hyper-db/docs/sql/

## exemple with dbeaver

![dbeaver](https://github.com/aetperf/HyperEngineService/blob/master/images/DBeaver_Connected_To_HyperEngine.jpg?raw=true)

