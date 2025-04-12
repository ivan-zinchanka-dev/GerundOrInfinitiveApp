CREATE TABLE Examples (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SourceSentence TEXT NOT NULL,
    UsedWord TEXT NOT NULL,
    CorrectAnswer TEXT NOT NULL
);

CREATE TABLE AlternativeCorrectAnswers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ExampleId INTEGER UNIQUE NOT NULL,
    Answer TEXT NOT NULL,
    
    FOREIGN KEY (ExampleId) REFERENCES Examples(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE LatestExampleResponses (
    ExampleId INTEGER PRIMARY KEY,
    Time INTEGER NOT NULL,
    Result INTEGER NOT NULL,

    FOREIGN KEY (ExampleId) REFERENCES Examples(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE VIEW ExamplesBatch AS
SELECT *
FROM Examples ex
         LEFT JOIN LatestExampleResponses resp ON ex.Id = resp.ExampleId
ORDER BY
    CASE WHEN resp.Result = 0 THEN 0 ELSE 1 END,
    resp.Time,
    RANDOM();

SELECT *
FROM ExamplesBatch
LIMIT 10;


SELECT * FROM Examples;

DELETE FROM Examples;

DROP VIEW ExamplesBatch;
DROP TABLE Examples;

DROP TABLE AlternativeCorrectAnswers;