# Family App
### Try me
Example credentials: login - test@test.pl, password - 12345678
[Login here](https://family.knasuta.me/account/login)

This app is implemented using ASP.NET Core 5.0 with Entity Framework Core. It allows users to share their To-Do and shopping list with their family members. They can also use a family chat to communicate. To join a family, you have to create an account and submit 10 digits "family code". You can join only one family at the same time. To leave family, you need to visit your account page (Account > Your name). You can also create a new family.

## Implementation

The app is written in ASP.NET Core. Data is stored in a MySQL database, using Entity Framework Core as an ORM framework. To create a live chat, I used SignalR. The frontend is created using Bootstrap 5.0.

### Configuration
.NET CLI

```
dotnet user-secrets init
dotnet user-secrets set "production_string" "Server = XXX; Database = XXX; Uid = XXX; Pwd = XXX;"
dotnet user-secrets set "development_string" "Server = XXX; Database = XXX; Uid = XXX; Pwd = XXX;"
```
