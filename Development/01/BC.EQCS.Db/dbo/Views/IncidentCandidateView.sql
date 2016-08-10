
CREATE VIEW [dbo].[IncidentCandidateView]
AS
SELECT 
	  Candidate.Id,
      Candidate.IncidentId,
      Candidate.Number,
      Candidate.Surname,
      Candidate.Firstnames,
      Candidate.Address,
      Candidate.DateOfBirth,
      Candidate.Gender,
      Candidate.IdDocumentNumber,
      Candidate.TrfNumber,
      Candidate.DateTrfCancelled,      
      Candidate.UKVIRefNumber,
	  Nationality.Name AS Nationality

	
FROM IncidentCandidates Candidate
LEFT JOIN Country AS Nationality
	ON Nationality.Id = Candidate.NationalityId
