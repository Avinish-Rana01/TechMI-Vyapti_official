# 📚 Markss Retail Suite (Vyapti) — Documentation Index

Welcome to the internal engineering memory and technical reference library for **Markss Retail Suite (Vyapti)**.

Use these documents to understand the architecture, page hierarchy, session models, and development rules:

---

### 📖 Documentation Directory

1. **[01. Architecture & System Flow](file:///c:/Users/MARKSS/OneDrive/Documents/ALL codes/Markss_Retail_Suite/md/01_ARCHITECTURE_AND_APPLICATION_FLOW.md)**
   * Complete architectural topology (.NET Framework 4.7.2, ADO.NET, SQL Server).
   * Request and authentication lifecycles.
   * 5 Master Pages mapping and view isolation mechanics.
   * Unified Single Sign-On (SSO) and 6-Card Module Hub design.
   * IIS Express & Port 8088 Network Proxy architecture.

2. **[02. Modules & Page Catalog](file:///c:/Users/MARKSS/OneDrive/Documents/ALL codes/Markss_Retail_Suite/md/02_MODULES_AND_PAGE_CATALOG.md)**
   * End-to-end supply chain workflow (Tag PO ➔ Work Order ➔ Encoding ➔ ASN ➔ Inbound ➔ QC ➔ Putaway ➔ GRC).
   * Exhaustive directory of all 50+ `.aspx` pages across:
     - `HQ_Operations` (14 Master Data catalogs + Operational workflows).
     - `Vendor_Management_System` (VMS).
     - `Warehouse_Management_System` (WMS).
     - `Store_Operations`.
     - `Configuration_Module`.
     - Root dashboards and invoices.

3. **[03. Session State & Database Guide](file:///c:/Users/MARKSS/OneDrive/Documents/ALL codes/Markss_Retail_Suite/md/03_SESSION_STATE_AND_DATABASE_GUIDE.md)**
   * Complete Session variable dictionary (`Session["User_ID"]`, `Session["MenuText"]`, roles, types).
   * Data access layer (`PatelHelper.cs` / `SqlHelper.ExecuteDataset`, `ExecuteNonQuery`).
   * Stored procedures catalog (`[SP_MASTER]`, `[SP_MASTER1]`, `[SP_EMPLOYEE_MASTER]`).
   * Database schemas, `User_Registration` table layout, and `Web.config` connection strings.

4. **[04. Developer Playbook & Safe Change Guidelines](file:///c:/Users/MARKSS/OneDrive/Documents/ALL codes/Markss_Retail_Suite/md/04_DEVELOPER_GUIDELINES_AND_CHANGE_PLAYBOOK.md)**
   * Invariants and non-negotiable development rules.
   * Step-by-step instructions for editing ASPX, Designer, and Code-Behind.
   * How to start the dev stack and expose it over the LAN (Port 8088).
   * Git commit hygiene and `.gitignore` safety.
   * Common pitfalls and troubleshooting matrix.
