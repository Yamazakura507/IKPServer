# Localization architecture

## Scope

The first localization stage introduces server-side language metadata and versioned language package metadata. The actual package bytes remain in the Files bounded context and are referenced through `FileId`.

```text
localization_lookup.Languages
        |
        +---- localization.LanguagePackages
                       |
                       +---- files.Files
```

System UI translations are intended to live in versioned client language packages rather than one database row per translated string. Business data that needs database-backed localization can be added later without coupling the language package mechanism to entity tables.

## Built-in languages

The migration seeds:

- `en` / `en-US` — English, default, system language.
- `ru` / `ru-RU` — Russian, system language.

## API

```text
POST /api/localization/languages/query
POST /api/localization/languages/{languageId}/packages/query
GET  /api/localization/languages/{languageId}/package
POST /api/localization/languages/{languageId}/packages
DELETE /api/localization/languages/{languageId}/packages/{packageId}
```

Language and package list endpoints use the existing Universal Query contract, including projection, filtering, search, sorting and pagination.

Package registration does not upload bytes. The client first uploads a file through Files and then registers that file as a language package. Registering a new active package automatically deactivates the previous active package for the same language.

Deleting a package is a metadata deactivation; the physical file is intentionally not deleted automatically because the Files context owns file lifecycle.
