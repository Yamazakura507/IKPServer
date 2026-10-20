# Updates architecture

The Updates bounded context stores metadata and relationships for clients, libraries and installers. Physical packages remain in the Files bounded context.

## Storage layout

The logical storage layout is represented by product metadata and file records:

- `Clients / Platform / Client / Version / File`
- `Libs / Library / Version / File`
- `Installers / Installer / Version / File`

The database does not depend on a separate table hierarchy for each category. `updates_lookup.UpdateCategories` contains the stable categories `Clients`, `Libs` and `Installers`; `updates.Products` represents an individual client/library/installer; `updates.Versions` represents its releases.

## Release information

`updates.ReleaseNotes` is shared by all three categories. A release note can be marked as planned (`IsPlanned`) so the same model can describe both changes already delivered and changes planned for a future version.

`updates.ReleaseNoteAttachments` links release notes to the Files bounded context, allowing images, videos and other supported files to be attached without duplicating file storage.

`updates.DownloadLinks` provides future external download sources. A version can also point directly to a stored file through `FileId`.

## Main tables

- `updates_lookup.UpdateCategories`
- `updates.Platforms`
- `updates.Products`
- `updates.Versions`
- `updates.ReleaseNotes`
- `updates.ReleaseNoteAttachments`
- `updates.DownloadLinks`

This keeps client/library/installer administration unified while preserving category-specific filtering.
