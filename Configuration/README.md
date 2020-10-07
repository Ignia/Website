Instructions
============

### Connection Strings
To connect to a database locally, establish a `secrets.json` at e.g.:
```
\Users\USERNAME\AppData\Roaming\Microsoft\UserSecrets\af60e1f6-3ef2-4ab5-bbcd-4a83055155c1\secrets.js
```

Then add the connection string using the following format:

```json
{
  "ConnectionStrings": {
    "OnTopic": "CONNECTION_STRING"
  }
}
```

Database
--------
To import a `dacpac` file downloaded from Azure, go to the following folder in the Visual Studio installation directory:
```
\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\140
```
And execute the following command:
```
SqlPackage.exe /a:import /tcs:"{ConnectionString}" /sf:"{Filename}.bacpac"
```