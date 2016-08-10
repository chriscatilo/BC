CREATE VIEW [dbo].[IncidentsListingView]
AS
SELECT DISTINCT 
                         inc.Id, 
						 inc.FormalId AS [Incident Number], 
						 tc.CentreNumber AS [Test Centre Number], 
						 dbo.Product.Name AS Product, 
						 COALESCE (category.Code, 
                         subCategoryParent.Code) AS CategoryCode, 
						 COALESCE (category.Name, 
						 subCategoryParent.Name) AS Category, 
						 COALESCE (subCategory.Name, category.Name) 
                         AS DisplayedCatOrSubCat, 
						 subCategory.Code AS SubCategoryCode, 
						 subCategory.Name AS SubCategory, 
						 inc.RaisedBy AS [Raised By], 
                         apUse.DisplayName AS [Logged By], 
						 inc.TestDate AS [Test Date], 
						 inc.IncidentDate AS [Incident Date], 
						 inc.Status, 
						 COALESCE (inS.StatusName, 'None') AS StatusName, 
						 inS.Code AS StatusCode,
                         (SELECT CASE WHEN EXISTS
                                (SELECT        *
                                FROM            dbo.IncidentActions ai
                                WHERE        ai.Status = 1 AND ai.IncidentId = inc.Id) THEN 'true' ELSE 'false' END AS Expr1) AS HasActiveAction, 
                         CASE WHEN inc.ReportUkvi = 1 THEN 'Yes' WHEN inc.ReportUkvi = 0 THEN 'No' ELSE 'N/A' END AS ReportUkvi, 
						 au1.Code AS VenueAdminUnitCode, 
                         au1.Id AS VenueAdminUnitId, 
						 inc.UkviFollowUpDate

FROM				dbo.Incident AS inc LEFT OUTER JOIN
                         dbo.ApplicationUser AS apUse ON inc.LoggedById = apUse.Id LEFT OUTER JOIN
                         dbo.TestLocation AS tl ON tl.Id = inc.TestLocationId LEFT OUTER JOIN
                         dbo.AdminUnit AS au1 ON au1.Id = tl.AdminUnitId LEFT OUTER JOIN
                         dbo.TestCentre AS tc ON tc.Id = inc.TestCentreId LEFT OUTER JOIN
                         dbo.IncidentClass AS ic ON inc.IncidentClassId = ic.Id LEFT OUTER JOIN
                         dbo.Product ON inc.ProductId = dbo.Product.Id LEFT OUTER JOIN
                         dbo.IncidentActions ON inc.Id = dbo.IncidentActions.IncidentId LEFT OUTER JOIN
                         dbo.IncidentStatus AS inS ON inc.Status = inS.Id LEFT OUTER JOIN
                         dbo.IncidentClass AS category ON inc.IncidentClassId = category.Id AND category.TypeId =
                             (SELECT        Id
                               FROM            dbo.IncidentClassType
                               WHERE        (Code = 'Category')) LEFT OUTER JOIN
                         dbo.IncidentClass AS subCategory ON inc.IncidentClassId = subCategory.Id AND subCategory.TypeId =
                             (SELECT        Id
                               FROM            dbo.IncidentClassType
                               WHERE        (Code = 'SubCategory')) LEFT OUTER JOIN
                         dbo.IncidentClass AS subCategoryParent ON subCategory.ParentId = subCategoryParent.Id AND subCategoryParent.TypeId =
                             (SELECT        Id
                               FROM            dbo.IncidentClassType
                               WHERE        (Code = 'Category'))
WHERE        (inc.Status <> 4)

GO