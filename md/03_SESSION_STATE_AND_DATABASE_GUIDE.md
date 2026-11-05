# 🗄️ Markss Retail Suite (Vyapti) — Session State, Stored Procedures & Database Guide

> **Document Version**: 1.0  
> **Target Scope**: Session State Dictionary, Data Access Layer (`SqlHelper`), Stored Procedures Specifications, and Database Schemas.

---

## 📑 Table of Contents
1. [Session State Variable Dictionary](#1-session-state-variable-dictionary)
2. [Data Access Architecture (SqlHelper & PatelHelper.cs)](#2-data-access-architecture-sqlhelper--patelhelpercs)
3. [Core Stored Procedures Catalog](#3-core-stored-procedures-catalog)
4. [Database Connection Configurations (Web.config)](#4-database-connection-configurations-webconfig)
5. [Core Database Tables & Schemas](#5-core-database-tables--schemas)

---

## 1. Session State Variable Dictionary

Every Master Page and content page in Markss Retail Suite relies on `System.Web.HttpContext.Current.Session`. Modifying or clearing any of these keys without proper fallback will trigger redirects to `index.aspx` or `NullReferenceException`.

| Session Key | Data Type | Populated By | Purpose & Consumer |
| :--- | :--- | :--- | :--- |
| `Session["User_ID"]` | `string` / `int` | `login.aspx.cs` / `index.aspx.cs` | Primary key of the authenticated user in `User_Registration`. Required by every Master page. |
| `Session["UserName"]` | `string` | `login.aspx.cs` | Display name of the user shown in top right profile header (`lbl_Username`). |
| `Session["MenuText"]` | `string` | `login.aspx.cs` / Hub Click | **CRITICAL INVARIANT:** Stores active module name (e.g. `"HQ Operations"`). Master pages inspect this to show/hide menus. |
| `Session["ROLE_NAME"]` | `string` | `login.aspx.cs` | Name of user's security role (e.g., `"Admin"`, `"HQ Manager"`). |
| `Session["ROLE_ID"]` | `string` / `int` | `login.aspx.cs` | Foreign key to `Role_Master`. |
| `Session["USER_CONFIG_TYPE"]`| `string` | `login.aspx.cs` | Configuration category (e.g. `"HQ"`, `"Store"`, `"Warehouse"`). |
| `Session["USER_TYPE"]` | `string` | `login.aspx.cs` | Distinguishes special external accounts (`"Tag Vendor"`, `"Vendor"` vs internal staff). |
| `Session["DEPT_NAME"]` | `string` | `login.aspx.cs` | Department title shown in profile dropdown (`lbl_Department`). |
| `Session["DESIGNATION"]` | `string` | `login.aspx.cs` | Job designation shown in profile dropdown (`lbl_Designation`). |
| `Session["EMAIL_ID"]` | `string` | `login.aspx.cs` | User's email address shown in profile dropdown (`lbl_Emailid`). |
| `Session["STORE_ID"]` | `string` / `int` | `login.aspx.cs` | Assigned store ID (for store users). |
| `Session["STORE_NAME"]` | `string` | `login.aspx.cs` | Name of the assigned store. |
| `Session["WAREHOUSE_ID"]` | `string` / `int` | `login.aspx.cs` | Assigned warehouse ID (for warehouse users). |
| `Session["UserAllowedModules"]`| `HashSet<string>` | Central SSO Gateway | *(New Unified Hub)* Stores the list of all modules the active user has rights to open. |

---

## 2. Data Access Architecture (`SqlHelper` & `PatelHelper.cs`)

The project uses a custom high-performance ADO.NET utility class located in `PatelHelper.cs` (`namespace Patel.Data`):

```csharp
using Patel.Data;
```

### Key Execution Methods

#### 1. `SqlHelper.ExecuteDataset` (For SELECT / Multi-Row Queries)
Used whenever a query returns tables, datasets, or dropdown values:
```csharp
List<SqlParameter> parameters = new List<SqlParameter>
{
    new SqlParameter("@Action", "GET_ACCESS_MENU_SUB_MENU"),
    new SqlParameter("@USER_ID", Session["USER_ID"].ToString().Trim()),
    new SqlParameter("@USER_MODULE_NAME", Module.ToString().Trim())
};

DataSet ds = SqlHelper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "[SP_MASTER]", parameters.ToArray());
if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
{
    // Consume data
}
```

#### 2. `SqlHelper.ExecuteNonQuery` (For INSERT / UPDATE / DELETE)
Used for data modifications:
```csharp
List<SqlParameter> param = new List<SqlParameter>
{
    new SqlParameter("@Action", "USER_STATUSUPDATE"),
    new SqlParameter("@USER_ID", id)
};

int rowsAffected = SqlHelper.ExecuteNonQuery(ConnStr, CommandType.StoredProcedure, "[SP_MASTER1]", param.ToArray());
```

---

## 3. Core Stored Procedures Catalog

The application relies on centralized multi-action stored procedures driven by an `@Action` parameter:

### A. `[SP_MASTER]`
Primary stored procedure for authentication and menu RBAC:

| `@Action` Parameter | Input Parameters | Output / Result Set |
| :--- | :--- | :--- |
| `LOGIN_CHECK` | `@USER_NAME`, `@PASSWORD`, `@USER_MODULE_NAME` | Returns `CODE` (`1` = Success, `2` = Inactive, `3` = Invalid), plus all user attributes (`USER_ID`, `EMAIL_ID`, `ROLE_NAME`, etc.). |
| `GET_ACCESS_MENU_SUB_MENU` | `@USER_ID`, `@USER_MODULE_NAME` | Returns `MENU_NAMES` and `SUBMENU_NAMES` as comma-separated lists for Master Page menu rendering. |

### B. `[SP_MASTER1]`
Primary stored procedure for User Management and Role Configuration:

| `@Action` Parameter | Input Parameters | Output / Result Set |
| :--- | :--- | :--- |
| `SHOW_USER_MASTER` | None | Returns all registered users for `grdUserMaster` in `User_Master.aspx`. |
| `DD_ROLE_MASTER` | None | Returns roles dropdown (`Role_ID`, `Role_Name`). |
| `DD_DEPARTMENT_MASTER`| None | Returns departments dropdown (`Dept_ID`, `Dept_Name`). |
| `DD_DESIGNATION_MASTER`| None | Returns designations dropdown (`Designation_ID`, `Designation_Name`). |
| `DD_STORE_MASTER` | None | Returns stores dropdown (`Store_ID`, `Store_Name`). |
| `DD_WAREHOUSE_MASTER` | None | Returns warehouses dropdown (`WH_ID`, `WH_Name`). |
| `INSERT_USER_MASTER` | `@EMPLOYEE_ID`, `@USER_NAME`, `@PASSWORD`, `@USER_MODULE_NAME`, `@USER_MENU_NAME`, `@USER_SUB_MENU_NAME`, `@USER_CONFIG_TYPE`, `@STORE_ID`, `@WAREHOUSE_ID` | Creates new user in `User_Registration`. Returns message in output parameter. |
| `UPDATE_USER_MASTER` | Same as insert + `@User_Reg_ID` | Updates existing user record and permissions. |
| `VIEW_USER_CONFIG_DETAILS` | `@USER_ID` | Returns table of assigned `MODULE_NAME`, `MENU_NAME`, and `SUB_MENU_NAME` for the user. |
| `USER_STATUSUPDATE` | `@USER_ID` | Toggles user status between Active/Inactive. |

### C. `[SP_EMPLOYEE_MASTER]`
Handles internal staff registration and employee bindings:
* `@Action = 'GET_DROPDOWN'`
* `@Action = 'GET_EMPLOYEE_DETAILS'`

---

## 4. Database Connection Configurations (`Web.config`)

In `Web.config`:
```xml
<connectionStrings>
    <!-- Primary SQL Server Connection String -->
    <add name="sqlconn" 
         connectionString="Data Source=DESKTOP-2VLE254;Initial Catalog=RETAIL_SUITE;User ID=sa;Password=M@rkss2026;Integrated Security=false;TrustServerCertificate=True" 
         providerName="System.Data.SqlClient" />
         
    <!-- Legacy Excel OLEDB Providers -->
    <add name="Excel03ConString" connectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'" />
    <add name="Excel07+ConString" connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'" />
</connectionStrings>
```

---

## 5. Core Database Tables & Schemas

### `User_Registration` Table
Governs credentials and core user identities:
* `User_ID` (`int`, Primary Key, Identity)
* `User_Name` (`nvarchar(50)`)
* `Password` (`nvarchar(50)`)
* `User_Type` (`nvarchar(50)`)
* `Store_ID` (`int`, Nullable)
* `WH_ID` (`int`, Nullable)
* `Role_ID` (`int`, Nullable)
* `User_Email_ID` (`nvarchar(100)`)
* `Is_Status` (`char(1)`)
* `Is_Login_Status` (`char(1)`)
* `Entry_Date` (`datetime`)
* `Entry_By` (`int`)
