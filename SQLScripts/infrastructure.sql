CREATE TABLE Examples (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SourceSentence TEXT NOT NULL,
    UsedWord TEXT NOT NULL,
    CorrectAnswer TEXT NOT NULL
);

CREATE TABLE AlternativeCorrectAnswers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ExampleId INTEGER NOT NULL,
    AlternativeCorrectAnswer TEXT NOT NULL,
    
    FOREIGN KEY (ExampleId) REFERENCES Examples(Id) ON DELETE CASCADE ON UPDATE CASCADE
);



INSERT INTO Examples (SourceSentence, UsedWord, CorrectAnswer)
VALUES
    ('In court the accused admitted (to) ... the documents.', 'steal', 'stealing'),
    ('I always try to avoid ... in the rush hour.', 'drive', 'driving'),
    ('It isn''t worth ... to the exhibition. It''s really boring.', 'go', 'going'),
    ('You look tired. Would you rather ... in this evening and watch a film?', 'stay', 'stay')

SELECT * FROM Examples


DELETE FROM Examples

DROP TABLE Examples

DROP TABLE AlternativeCorrectAnswers