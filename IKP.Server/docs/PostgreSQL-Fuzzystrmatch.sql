-- Run in the target PostgreSQL database from DBeaver.
-- The connected role must have permission to create extensions.
CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;

-- Verify that the extension is installed.
SELECT extname, extversion
FROM pg_extension
WHERE extname = 'fuzzystrmatch';

-- Verify Levenshtein is available.
SELECT levenshtein('kitten', 'sitting') AS distance;
