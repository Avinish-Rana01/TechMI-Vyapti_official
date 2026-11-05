# 🛠️ Markss Retail Suite (Vyapti) — Developer Playbook & Safe Change Guidelines

> **Document Version**: 1.0  
> **Target Scope**: Safe Change Procedures, ASP.NET Web Forms Editing Rules, Git Hygiene, and Network Testing Playbook.

---

## 📑 Table of Contents
1. [Core Development Rules & Guardrails](#1-core-development-rules--guardrails)
2. [Step-by-Step Procedure for Modifying Web Forms](#2-step-by-step-procedure-for-modifying-web-forms)
3. [How to Run and Test Over the Local Network](#3-how-to-run-and-test-over-the-local-network)
4. [Git & Repository Protection Rules](#4-git--repository-protection-rules)
5. [Common Pitfalls & Troubleshooting Checklist](#5-common-pitfalls--troubleshooting-checklist)

---

## 1. Core Development Rules & Guardrails

When working on Markss Retail Suite:
1. **Never Break Session Continuity:** Master Pages strictly validate `Session["MenuText"]`, `Session["User_ID"]`, and `Session["UserName"]`. Any navigation code that redirects to a module without setting `Session["MenuText"]` will cause an instant redirect back to `index.aspx`.
2. **Never Commit Build Binaries or IDE Caches:** `.gitignore` must continuously exclude `bin/`, `obj/`, `packages/`, `.vs/`, and `*.user`. The clean repo size is **~30.5 MB**; committing build artifacts inflates it to **169 MB**.
3. **Preserve Custom Libraries:** `lib/QueryStringEncryption.dll` must remain in `lib/` because it is an unmanaged, non-NuGet dependency required for query string security.
4. **Standard Hex Colors:** When styling alerts, buttons, or charts, adhere to standardized hex codes:
   * **Blue:** `#3B82F6`
   * **Red:** `#EF5350`
   * **Green:** `#10B981`

---

## 2. Step-by-Step Procedure for Modifying Web Forms

Every ASP.NET Web Forms screen is split into three files:
1. `PageName.aspx` (Markup & Client Controls)
2. `PageName.aspx.cs` (C# Code-Behind & Event Handlers)
3. `PageName.aspx.designer.cs` (Automatic Control Declarations)

### Rules for Safe Edits:
* **Adding Server Controls:** When adding `<asp:Button>`, `<asp:TextBox>`, or `<asp:Panel>` to an `.aspx` page, always declare them in `PageName.aspx.designer.cs` if working outside Visual Studio, or use existing server control IDs to prevent compile-time missing field errors.
* **HTML Generic Controls (`runat="server"`):** Use `<div id="divId" runat="server">` or `<span id="spanId" runat="server">` when you need to toggle visibility (`Visible = true/false`) or inject CSS classes (`Attributes["class"] = "..."`) directly from C#.
* **Event Handlers:** Never delete an event handler attribute (e.g. `OnClick="btnSave_Click"`) from `.aspx` without removing or replacing the corresponding method in `.aspx.cs`, otherwise a runtime parser exception will be thrown.

---

## 3. How to Run and Test Over the Local Network

Because IIS Express does not accept external network requests by default, always run the dev stack in two synchronized steps:

### Step 1: Start IIS Express (Background Process)
```powershell
& "C:\Program Files\IIS Express\iisexpress.exe" /path:"c:\Users\MARKSS\OneDrive\Documents\ALL codes\Markss_Retail_Suite" /port:58440 /systray:false
```

### Step 2: Start Network Proxy (Port 8088)
Runs `network_proxy.js` to listen on `0.0.0.0:8088` and forward traffic to `127.0.0.1:58440` with rewritten host headers:
```powershell
node "C:\Users\MARKSS\.gemini\antigravity\brain\c90017be-064c-4d63-9f9f-16f6a603663b\scratch\network_proxy.js"
```

### Step 3: Access URLs
* **Local Machine:** `http://localhost:58440/index.aspx`
* **Network (Other PCs / Wi-Fi):** `http://172.20.204.134:8088/index.aspx`

---

## 4. Git & Repository Protection Rules

* **Remote Origin:** `https://github.com/Avinish-Rana01/TechMI-Vyapti_official.git`
* **Default Branch:** `main`
* **Pre-Commit Verification:** Always run `git status --short` before committing. If you see `bin/`, `obj/`, or `.vs/` marked as untracked (`??`), STOP immediately and verify `.gitignore`.

---

## 5. Common Pitfalls & Troubleshooting Checklist

| Symptom | Root Cause | Fix |
| :--- | :--- | :--- |
| **Redirects to `index.aspx` immediately after clicking a link** | `Session["MenuText"]` is null or was not set before redirecting to the module. | Set `Session["MenuText"] = "<ModuleName>"` prior to `Response.Redirect()`. |
| **HTTP Error 400 (Invalid Hostname)** | Client requested IIS Express via network IP (`http://172.20.204.134:58440`) instead of the proxy. | Ensure external computers access via port **8088** (`http://172.20.204.134:8088/`). |
| **SweetAlert / Toastr not rendering** | Missing asset link in the `<head>` of the page. | Ensure `assets/plugins/global/plugins.bundle.css` and `assets/plugins/global/plugins.bundle.js` are referenced. |
| **Missing Reference to `QueryStringEncryption` on fresh clone** | The non-NuGet DLL was not restored. | Reference `lib/QueryStringEncryption.dll` in `.csproj`. |
