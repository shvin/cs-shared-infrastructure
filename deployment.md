# Deployment Guide — CS.Infrastructure

This package is published to GitHub Packages automatically via GitHub Actions when changes are merged to `main` or a version tag is pushed.

---

## First-Time Setup

Before pushing or tagging, make sure your local repo is up to date:

1. Open the solution in **Visual Studio**
2. Go to **Git** menu → **Fetch**
3. Go to **Git** menu → **Pull**

---

## Publishing a New Version

### Step 1 — Update the version number

Open `CS.Infrastructure.csproj` and bump the `<Version>` value:

```xml
<Version>1.1.0</Version>
```

Commit the change:
- Go to **Git** menu → **Commit or Stash**
- Enter a message like `chore: bump version to 1.1.0`
- Click **Commit All**

---

### Step 2 — Merge to main

1. Go to **Git** menu → **Manage Branches**
2. Double-click **main** to check it out
3. Right-click your feature branch → **Merge into current branch**
4. Go to **Git** menu → **Push**

This push triggers the pipeline. The package is built and published to GitHub Packages automatically using the version number in the csproj.

---

### Step 3 — Tag the release (optional but recommended)

Tagging creates a clean, immutable release and publishes the package using the tag as the version (overrides the csproj version).

1. Go to **Git** menu → **View** → **Git Repository**
2. In the left panel, click **Tags**
3. Click **New Tag**
4. Set the name to match the version — e.g. `v1.1.0`
5. Leave the target on the latest commit on `main`
6. Click **Create Tag**
7. Right-click the new tag → **Push**

---

## Monitoring the Pipeline

After pushing, watch the build at:

**https://github.com/shvin/cs-shared-infrastructure/actions**

A green check means the package was published successfully.

---

## Installing the Package in Another Project

Add the GitHub Packages feed to your NuGet sources (one-time setup per machine):

1. In Visual Studio, go to **Tools** → **NuGet Package Manager** → **Package Manager Settings**
2. Select **Package Sources** → click **+**
3. Set:
   - **Name:** `CS GitHub Packages`
   - **Source:** `https://nuget.pkg.github.com/shvin/index.json`
4. Click **Update** → **OK**

When prompted for credentials, use:
- **Username:** your GitHub username
- **Password:** a GitHub Personal Access Token with `read:packages` scope

Then install as normal via **Manage NuGet Packages** and search for `CS.Infrastructure`.

> **Note:** `CS.Infrastructure` does not depend on `CS.Base` as a NuGet package reference — the types are self-contained. Consuming projects that need both should reference each package independently.
