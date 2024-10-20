BEGIN TRY
	CREATE DATABASE ShortenedLink;

	USE ShortenedLink;

	CREATE TABLE ShortenedLink (
		Id INT NOT NULL IDENTITY(1,1),       
		OriginalUrl VARCHAR(255) NOT NULL,   
		ShortUrl VARCHAR(255) NOT NULL,      
		PRIMARY KEY (Id)                     
	);
	
END TRY
BEGIN CATCH
    PRINT 'An error occurred';
    PRINT ERROR_MESSAGE();
END CATCH;